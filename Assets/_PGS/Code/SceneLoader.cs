using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using MEC;

namespace PGS
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Image m_LoadingImage;
        [SerializeField] private TextMeshProUGUI m_LoadingText;

        private CoroutineHandle m_LoadCoroutine;

        public Action<SceneReference, TimeSpan> OnSceneLoadStart;
        public Action<SceneReference, TimeSpan> OnSceneLoadEnd;
        public Action<TimeSpan> OnAllScenesLoaded;

        /// <summary>
        /// The queue of Scenes we need to load and want to keep track of the status of
        /// </summary>
        private Queue<SceneData> m_SceneQueue = new Queue<SceneData>();

        private IEnumerator<float> Load()
        {
            Debug.Log($"Total Scenes Queued to Load: {m_SceneQueue.Count}");
            m_Canvas.gameObject.SetActive(true); //Play the intro animation

            while (m_SceneQueue.Count > 0)
            {
                SceneData scene = DequeueScene();

				if (scene.SceneRef.scene.IsValid() && !scene.SceneRef.IsLoaded)
                {
					OnSceneLoadStart?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene.SceneRef.SceneName, LoadSceneMode.Additive));
                    yield return Timing.WaitForOneFrame;
                    OnSceneLoadEnd?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
				}
            }

            OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
			m_Canvas.gameObject.SetActive(false); //Play the outro animation
            m_SceneQueue.Clear();
		}

        public void QueueSceneLoad(SceneData scene)
        {
            if (m_SceneQueue.Contains(scene)) { return; }
            m_SceneQueue.Enqueue(scene);
        }

        private SceneData DequeueScene()
        {
            return m_SceneQueue.Dequeue();
        }

        public void LoadScene(SceneData scene)
        {
            QueueSceneLoad(scene);
            Load();
        }

        public void LoadScenes(SceneData[] scenes)
        {
            foreach (SceneData scene in scenes)
            {
                QueueSceneLoad(scene);
            }

			m_LoadCoroutine = Timing.RunCoroutineSingleton(Load(), m_LoadCoroutine, SingletonBehavior.Wait);
        }
    }
}
