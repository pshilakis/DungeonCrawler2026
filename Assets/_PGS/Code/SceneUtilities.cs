using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;

namespace PGS
{
    public static class SceneUtilities
    {
		private static SceneLoader m_SceneLoader;
		private static CoroutineHandle m_LoadCoroutine;

		public static Action<SceneReference, TimeSpan> OnSceneLoadStart;
		public static Action<SceneReference, TimeSpan> OnSceneLoadEnd;
		public static Action<TimeSpan> OnAllScenesLoaded;

		/// <summary>
		/// The queue of Scenes we need to load and want to keep track of the status of
		/// </summary>
		private static readonly Queue<SceneData> m_SceneQueue = new Queue<SceneData>();

		public static void RegisterSceneLoader(SceneLoader loader)
		{
			m_SceneLoader = loader;
			Debug.Log("Scene Loader Registered!");
		}

		private static IEnumerator<float> Load()
		{
			Debug.Log($"Total Scenes Queued to Load: {m_SceneQueue.Count}");
			//m_Canvas.gameObject.SetActive(true); //Play the intro animation

			while (m_SceneQueue.Count > 0)
			{
				SceneData scene = DequeueScene();

				if (!scene.SceneRef.IsLoaded)
				{
					OnSceneLoadStart?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene.SceneRef.SceneName, LoadSceneMode.Additive));
					yield return Timing.WaitForOneFrame;
					OnSceneLoadEnd?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
				}
			}

			OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
			//m_Canvas.gameObject.SetActive(false); //Play the outro animation
			m_SceneQueue.Clear();
		}

		public static void QueueSceneLoad(SceneData scene)
		{
			if (m_SceneQueue.Contains(scene)) { return; }
			m_SceneQueue.Enqueue(scene);
		}

		private static SceneData DequeueScene()
		{
			Debug.Log("dequeue");
			Debug.Log(m_SceneQueue.Peek());

			return m_SceneQueue.Dequeue();
		}

		public static void LoadScene(SceneData scene)
		{
			QueueSceneLoad(scene);
			m_LoadCoroutine = Timing.RunCoroutineSingleton(Load(), m_LoadCoroutine, SingletonBehavior.Wait);
		}

		public static void LoadScenes(SceneData[] scenes)
		{
			foreach (SceneData scene in scenes)
			{
				QueueSceneLoad(scene);
			}

			m_LoadCoroutine = Timing.RunCoroutineSingleton(Load(), m_LoadCoroutine, SingletonBehavior.Wait);
		}
	}
}
