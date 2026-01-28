using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using MEC;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace PGS
{
    public static class SceneUtilities
    {
		private static SceneLoader m_SceneLoader;

		public static Action<SceneData, TimeSpan> OnSceneLoadStart;
		public static Action<SceneData, TimeSpan> OnSceneLoadEnd;
		public static Action<TimeSpan> OnAllScenesLoaded;

		/// <summary>
		/// A List of the current Scenes that we have loaded
		/// </summary>
		private static List<SceneData> m_LoadedScenes = new List<SceneData>();
		private static UniTask[] tasks;

		public static void RegisterSceneLoader(SceneLoader loader)
		{
			m_SceneLoader = loader;
			Debug.Log("Scene Loader Registered!");
		}

		#region Show/Hide Loading Screen
		public static bool LoadingScreenEnabled { get { return m_SceneLoader.Enabled; } }

		public static async UniTask ShowLoadScreen(bool playIntroAnimation)
		{
			await m_SceneLoader.Show(playIntroAnimation);
		}

		public static async UniTask HideLoadScreen(bool playOutroAnimation)
		{
			await m_SceneLoader.Hide(playOutroAnimation);
		}
		#endregion

		#region Load/Unload Scenes
		public static async UniTask LoadScenes(SceneData[] scenes, CancellationToken ct = default)
		{
			tasks = new UniTask[scenes.Length];
			for (int i = 0;	i < scenes.Length; i++)
			{
				if (scenes[i] != null && scenes[i].CanBeLoaded())
				{
					m_LoadedScenes.Add(scenes[i]);
				}

				LoadSceneMode mode = i == 0 ? LoadSceneMode.Single : LoadSceneMode.Additive;
				tasks[i] = scenes[i].Load(mode, ct);
			}

			await UniTask.WhenAll(tasks);
			OnAllScenesLoaded?.Invoke(DateTime.Now.TimeOfDay);
		}

		/// <summary>
		/// Loads a single scene additively
		/// </summary>
		/// <param name="scene"></param>
		/// <param name="ct"></param>
		/// <returns></returns>
		public static async UniTask LoadSceneAdditive(SceneData scene, bool setActive = true, CancellationToken ct = default)
		{
			if (scene == null)
			{
				Debug.LogError($"Trying to Load Scene Additively but the SceneData is null");
				return;
			}

			if (scene.CanBeLoaded())
			{
				await scene.Load(LoadSceneMode.Additive, ct);

				if (setActive)
				{
					Scene sc = SceneManager.GetSceneByName(scene.SceneName);
					SceneManager.SetActiveScene(sc);
				}
			}
		}

		public static async UniTask UnloadScene(SceneData scene)
		{
			await scene.Unload();
		}

		public static async UniTask UnloadAllScenes(CancellationToken ct = default)
		{
			if (m_LoadedScenes.Count == 0) { return; }
			Debug.Log($"Unloading {m_LoadedScenes.Count} Scenes");

			tasks = new UniTask[m_LoadedScenes.Count];
			for (int i = 0; i < m_LoadedScenes.Count; i++)
			{
				tasks[i] = m_LoadedScenes[i].Unload(ct);
			}

			await UniTask.WhenAll(tasks);

		}
		#endregion
	}
}
