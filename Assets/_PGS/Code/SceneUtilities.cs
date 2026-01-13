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
		public static async Task ShowLoadScreen(bool playIntroAnimation)
		{
			await m_SceneLoader.Show(playIntroAnimation);
		}

		public static async Task HideLoadScreen(bool playOutroAnimation)
		{
			await m_SceneLoader.Hide(playOutroAnimation);
		}
		#endregion

		#region Load/Unload Scenes
		private static IEnumerator<float> Load()
		{
			int totalSceneCount = m_LoadQueue.Count;
			Debug.Log($"Total Scenes Queued to Load: {totalSceneCount}");
			//yield return Timing.WaitUntilDone(m_SceneLoader.Show());

			while (m_LoadQueue.Count > 0)
			{
				int sceneNum = m_LoadQueue.Count;
				SceneData scene = m_LoadQueue.Dequeue();

				if (scene.SceneRef.Status != SceneReference.SceneStatus.LOADED)
				{
					Debug.Log($"<color=#00ff00>Loading Scene</color> : {scene.SceneRef.SceneName} ({sceneNum}/{totalSceneCount}) @ {DateTime.Now.TimeOfDay}");

					OnSceneLoadStart?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					scene.SetStatus(SceneReference.SceneStatus.LOADING);
					yield return Timing.WaitUntilDone(SceneManager.LoadSceneAsync(scene.SceneRef.SceneName, LoadSceneMode.Additive));
					yield return Timing.WaitForOneFrame;
					OnSceneLoadEnd?.Invoke(scene.SceneRef, DateTime.Now.TimeOfDay);
					scene.SetStatus(SceneReference.SceneStatus.LOADED);
				}
			}

			OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
			//yield return Timing.WaitUntilDone(m_SceneLoader.Hide());
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
			
		}
		#endregion
	}
}
