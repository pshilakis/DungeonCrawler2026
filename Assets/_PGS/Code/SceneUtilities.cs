using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
		private static readonly Queue<SceneData> m_LoadQueue = new Queue<SceneData>();
		private static readonly Queue<SceneData> m_UnloadQueue = new Queue<SceneData>();

		public static void RegisterSceneLoader(SceneLoader loader)
		{
			m_SceneLoader = loader;
			Debug.Log("Scene Loader Registered!");
		}

		#region Show/Hide Loading Screen
		public static CoroutineHandle ShowLoadScreen(bool playIntroAnimation)
		{
			return Timing.RunCoroutine(m_SceneLoader.Show(playIntroAnimation));
		}

		public static CoroutineHandle HideLoadScreen(bool playOutroAnimation)
		{
			return Timing.RunCoroutine(m_SceneLoader.Hide(playOutroAnimation));
		}
		#endregion

		#region Load/Unload Scenes
		private static IEnumerator<float> Load()
		{
			int totalSceneCount = m_LoadQueue.Count;
			Debug.Log($"Total Scenes Queued to Load: {totalSceneCount}");

			while (m_LoadQueue.Count > 0)
			{
				int sceneNum = m_LoadQueue.Count;
				SceneData scene = m_LoadQueue.Dequeue();

				if (scene.SceneRef.Status != SceneReference.SceneStatus.LOADED)
				{
					Debug.Log($"<color=#00ff00>Loading Scene ({sceneNum}/{totalSceneCount})</color>: {scene.SceneRef.SceneName} @ {DateTime.Now.TimeOfDay}");

					OnSceneLoadStart?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					scene.SetStatus(SceneReference.SceneStatus.LOADING);
					AsyncOperation op = SceneManager.LoadSceneAsync(scene.SceneRef.SceneName, LoadSceneMode.Additive);
					yield return Timing.WaitUntilDone(op);
					OnSceneLoadEnd?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					scene.SetStatus(SceneReference.SceneStatus.LOADED);

					yield return Timing.WaitForOneFrame;
				}
			}

			OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
			m_LoadQueue.Clear();
		}

		private static IEnumerator<float> Unload()
		{
			int totalSceneCount = m_UnloadQueue.Count;
			while(m_UnloadQueue.Count > 0)
			{
				int sceneNum = m_UnloadQueue.Count;
				SceneData scene = m_UnloadQueue.Dequeue();

				yield return Timing.WaitUntilDone(SceneManager.UnloadSceneAsync(scene.SceneRef.SceneName));
			}
		}

		public static void QueueSceneLoad(SceneData scene)
		{
			if (m_LoadQueue.Contains(scene)) { return; }
			m_LoadQueue.Enqueue(scene);
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

		public static void UnloadScene(SceneData scene)
		{

		}

		public static void UnloadAllScenes()
		{
			Debug.Log("Unloading Scenes");
		}
		#endregion
	}
}
