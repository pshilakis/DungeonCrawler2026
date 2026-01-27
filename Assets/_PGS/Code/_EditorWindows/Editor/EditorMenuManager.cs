using UnityEditor;
using UnityEditor.SceneManagement;

namespace PGS.Editor
{
    [InitializeOnLoad]
    public static class EditorMenuManager
    {
        #region Hotkey Shortcuts
        //Individual keys
        private const string HOTKEY = " "; //Every hotkey needs to be prefixed with an underscore
        private const string CTRL = "%";
        private const string ALT = "&";
        private const string SHIFT = "#";

		//Full shortcuts
		private const string NO_MODIFIER = HOTKEY + "_";
		private const string CTRL_SHIFT = HOTKEY + CTRL + SHIFT;
        private const string ALT_SHIFT = HOTKEY + ALT + SHIFT;
        private const string CRTL_ALT_SHIFT = HOTKEY + CTRL + ALT + SHIFT;
        #endregion

        /// <summary>
        /// The main menu button name
        /// </summary>
        private const string MAIN = "PGS/";

		#region Scene Bootstrap
		private const string SCENE_BOOTSTRAPPER = "Run Game From Boot Scene";
        private const string SCENE_BOOTSTRAP_PLAYERPREF = "RunGameFromStartupScene";
        public static bool SceneBootstrapEnabled
        {
            get { return EditorPrefs.GetBool(SCENE_BOOTSTRAP_PLAYERPREF, true); }
            set { EditorPrefs.SetBool(SCENE_BOOTSTRAP_PLAYERPREF, value); }
        }

        [MenuItem(MAIN + SCENE_BOOTSTRAPPER + CTRL_SHIFT + "R")]
        private static void ForceGameToRunFromStartup()
        {
			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				SceneBootstrapEnabled = true;
				EditorStartupLoader.RunGameFromBoot();
			}
		}

		//[MenuItem(MAIN + SCENE_BOOTSTRAPPER, true)]
		//private static bool ForceGameToRunFromStartupValidate()
		//{
		//    Menu.SetChecked(MAIN + SCENE_BOOTSTRAPPER, SceneBootstrapEnabled);
		//    return true;
		//}
		#endregion

		#region Editor Scene Loader Editor
		private const string TOOLS_SUBMENU = "Tools/";
		private const string EDITOR_SCENE_LOADER_EDITOR = "Editor Scene Loader";
		[MenuItem(MAIN + TOOLS_SUBMENU + EDITOR_SCENE_LOADER_EDITOR)]
		private static void OpenEditorSceneLoaderEditor()
		{
			EditorSceneLoader.ShowWindow();
		}
		#endregion
	}
}
