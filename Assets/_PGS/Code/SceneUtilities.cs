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
			int totalSceneCount = m_SceneQueue.Count;
			Debug.Log($"Total Scenes Queued to Load: {totalSceneCount}");
			yield return Timing.WaitUntilDone(m_SceneLoader.Show());

			while (m_SceneQueue.Count > 0)
			{
				int sceneNum = m_SceneQueue.Count;
				SceneData scene = DequeueScene();

				if (!scene.SceneRef.IsLoaded)
				{
					Debug.Log($"Loading Scene : {scene.SceneRef.SceneName} ({sceneNum}/{totalSceneCount}) @ {DateTime.Now.TimeOfDay}");
					OnSceneLoadStart?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene.SceneRef.SceneName, LoadSceneMode.Additive));
					yield return Timing.WaitForOneFrame;
					OnSceneLoadEnd?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
				}
			}

			OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
			yield return Timing.WaitUntilDone(m_SceneLoader.Hide());
			m_SceneQueue.Clear();
		}

		public static void QueueSceneLoad(SceneData scene)
		{
			if (m_SceneQueue.Contains(scene)) { return; }
			m_SceneQueue.Enqueue(scene);
		}

		private static SceneData DequeueScene()
		{
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
