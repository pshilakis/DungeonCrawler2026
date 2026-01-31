using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace PGS.Editor
{
    [InitializeOnLoad]
    public static class EditorStartupLoader
    {
        private const string dataGUID = "0d6c35c6b20b2ee459569166931a73dd";
		private static SavedSceneData data;

		static EditorStartupLoader()
        {
			EditorApplication.playModeStateChanged += LoadBootScene;
            string dataPath = AssetDatabase.GUIDToAssetPath(dataGUID);
			data = AssetDatabase.LoadAssetAtPath<SavedSceneData>(dataPath);
		}

		
        public static void RunGameFromBoot() //This is run by the MenuManager
        {
			GetCurrentOpenScenes(); //Get the current open scenes in the editor before starting playmode
			string bootScenePath = AssetDatabase.GetAssetPath(data.bootScene);
			EditorSceneManager.OpenScene(bootScenePath);
			EditorApplication.isPlaying = true;
		}

		/// <summary>
		/// Stores the currently loaded scenes in the data object before switching to play mode
		/// </summary>
		public static void GetCurrentOpenScenes()
		{
			int scenes = SceneManager.loadedSceneCount;
			Scene activeScene = EditorSceneManager.GetActiveScene(); //Store the active scene so we can reset to it later
			for (int i = 0; i < scenes; i++)
			{
				Scene scene = EditorSceneManager.GetSceneAt(i);
				string scenePath = scene.path;
				string sceneGUID = AssetDatabase.AssetPathToGUID(scenePath);
				bool active = activeScene.name == scene.name;

				data.AddGUID(sceneGUID, active);
			}
		}

		private static void LoadPreviousOpenScenes()
		{
			for (int i = 0; i < data.SavedSceneList.Count; i++)
			{
				string path = AssetDatabase.GUIDToAssetPath(data.SavedSceneList[i].sceneGUID);
				OpenSceneMode mode = i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive;
				Scene scene = EditorSceneManager.OpenScene(path, mode);

				if (data.SavedSceneList[i].wasActiveScene)
				{
					EditorSceneManager.SetActiveScene(scene);
				}
			}

			data.ClearGUIDList();
			EditorMenuManager.SceneBootstrapEnabled = false;
		}

		/// <summary>
		/// If the correct setting is checked, then we want to load the game from the startup scene only instead of whatever scenes we currently have open
		/// </summary>
		private static void LoadBootScene(PlayModeStateChange state)
        {
            //Debug.Log($"PLAY STATE CHANGE @ {UnityEngine.Time.time}");
            if (!EditorMenuManager.SceneBootstrapEnabled)
            {
				return;
            }

			switch (state)
            {
                case PlayModeStateChange.ExitingEditMode:
					//Debug.Log($"EDITING EDIT MODE @ {UnityEngine.Time.time}");
					break;

                case PlayModeStateChange.EnteredPlayMode:
					//Debug.Log(data.SavedSceneList.Count);
					//Debug.Log($"ENTERING PLAY MODE @ {UnityEngine.Time.time}");
					//EditorSceneManager.LoadScene(0);
					break;

                case PlayModeStateChange.EnteredEditMode:
                    //Debug.Log($"<color=green>[{nameof(EditorStartupLoader)}]</color> Entering Edit Mode\n> Loading {data.SavedSceneList.Count} previously opened scenes.");
                    LoadPreviousOpenScenes();
                    break;
            }
        }
    }
}
