using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

//https://www.youtube.com/watch?v=yqneLnM8syk&ab_channel=WarpedImagination | For making custom scene view overlays cuz that's the cool way to display this

/// <summary>
/// EditorWindow to dynamically get all scenes in the project and displaying them in a dropdown for easier loading
/// </summary>
public class EditorSceneLoader : PGSEditorWindowUGUI<EditorSceneLoader>
{
    private string[] m_SearchDirectory = new string[]
    {
		"Assets/_PGS/Game"
	};

	// Use GUID string as dictionary key (stable) and keep a separate ordered list for GUI iteration
	private Dictionary<string, EditorBuildSettingsScene> BuildSettingSceneDictionary = new Dictionary<string, EditorBuildSettingsScene>();
	private List<EditorObjectReference> SceneList = new List<EditorObjectReference>();


    private void OnEnable()
    {
        GetSceneAssetsInDirectoryAndBuildSettings(); //update the scenes dictionary
        EditorApplication.projectChanged += GetSceneAssetsInDirectoryAndBuildSettings;
		EditorBuildSettings.sceneListChanged += Repaint;
		EditorSceneManager.sceneOpened += Repaint;
		EditorSceneManager.sceneClosed += Repaint;
    }

	private void OnDisable()
    {
		EditorApplication.projectChanged -= GetSceneAssetsInDirectoryAndBuildSettings;
		EditorBuildSettings.sceneListChanged -= Repaint;
		EditorSceneManager.sceneOpened -= Repaint;
		EditorSceneManager.sceneClosed -= Repaint;
	}

    protected override void OnGUI()
    {
		foreach (EditorObjectReference entry in SceneList.ToList())//Iterate a copy of the scene list so rebuilding the dictionary won't break enumeration
		{
			if (Application.isPlaying)
			{
				GUI.enabled = false;
			}

			// snapshot current build scene if present
			BuildSettingSceneDictionary.TryGetValue(entry.GUID, out EditorBuildSettingsScene currentBuildScene);

			EditorGUILayout.BeginHorizontal();
			DrawBuildSettingButton(entry, currentBuildScene);
			DrawSceneAssetField(entry.Asset);
			DrawSceneOpenButton(entry.Asset);
			DrawSceneAddButton(entry.Asset);
			EditorGUILayout.EndHorizontal();

			GUI.enabled = true;
		}
	}

	private void Repaint(Scene scene, OpenSceneMode mode)
	{
		Repaint(scene);
	}

	private void Repaint(Scene scene)
	{
		Repaint();
	}

	private void GetSceneAssetsInDirectoryAndBuildSettings()
	{
		if (BuildSettingSceneDictionary.Count > 0) { BuildSettingSceneDictionary.Clear(); }
		if (SceneList.Count > 0) { SceneList.Clear(); }

		//Get all scenes in the directory we want to search
		HashSet<string> guids = AssetDatabase.FindAssets($"t:{typeof(SceneAsset).Name}", m_SearchDirectory).ToHashSet(); //Need the typeof(T).Name here otherwise typeof(SceneAsset) returns "UnityEditor.SceneAsset" which does not work with the filter; we need to remove the namespace

		//Get all scenes in buildSettings (in case any are outside of the search directory)
		EditorBuildSettingsScene[] scenesInBuild = EditorBuildSettings.scenes;
		foreach (EditorBuildSettingsScene scene in scenesInBuild)
		{
			guids.Add(scene.guid.ToString());
		}

		List<EditorObjectReference> scenesInProject = new List<EditorObjectReference>();
		foreach (string guid in guids)
		{
			scenesInProject.Add(new EditorObjectReference(guid));
		}

		scenesInProject = scenesInProject.OrderBy(x => x.Asset.name).ToList(); //Sort the list

		for (int i = 0; i < scenesInProject.Count; i++)// populate SceneList and dictionary keyed by GUID string
		{
			EditorObjectReference objRef = scenesInProject[i];
			SceneList.Add(objRef);

			EditorBuildSettingsScene buildSettingScene = GetBuildSettingSceneFromGUID(objRef.GUID);
			BuildSettingSceneDictionary[objRef.GUID] = buildSettingScene;
		}

		//Debug.Log($"{BuildSettingSceneDictionary.Count} SceneAssets Found in Directory \"{m_SearchDirectory[0]}\" & BuildSettings");
	}

	/// <summary>
	/// Search the BuildProfiles Scene List and return the EditorBuildSettingsScene if it exists, or null if it does not
	/// </summary>
	/// <param name="guid">The GUID of the asset we want to find</param>
	/// <returns></returns>
	private EditorBuildSettingsScene GetBuildSettingSceneFromGUID(string guid)
	{
		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{
			if (EditorBuildSettings.scenes[i].guid.ToString() == guid)
			{
				return EditorBuildSettings.scenes[i];
			}
		}

		return null;
	}

	private void DrawBuildSettingButton(EditorObjectReference obj, EditorBuildSettingsScene currentBuildScene)
	{
		float buttonHeight = EditorGUIUtility.singleLineHeight + 2f;
		const float buttonWidth = 150f;

		const string iconInBuild = "SceneAsset Icon";
		const string tooltipInBuild = "This Scene is included in BuildSettings";

		const string iconNotInBuild = "SceneAsset On Icon";
		const string tooltipNotInBuild = "Click to add this Scene to BuildSettings";

		GUIContent notInBuildContent = new GUIContent();
		notInBuildContent.image = EditorGUIUtility.IconContent(iconNotInBuild).image;
		notInBuildContent.text = "Not In Build";
		notInBuildContent.tooltip = tooltipNotInBuild;

		GUIContent enabledContent = new GUIContent();
		enabledContent.image = EditorGUIUtility.IconContent(iconInBuild).image;
		enabledContent.text = "In Build [Enabled]";
		enabledContent.tooltip = tooltipInBuild;

		GUIContent disabledContent = new GUIContent();
		disabledContent.image = EditorGUIUtility.IconContent(iconInBuild).image;
		disabledContent.text = "In Build [Disabled]";
		disabledContent.tooltip = tooltipInBuild;

		GUIContent[] dropdownButtons = {
			notInBuildContent,	//0
			enabledContent,		//1
			disabledContent		//2
		};

		int index = currentBuildScene != null ? (currentBuildScene.enabled ? 1 : 2) : 0; // Use snapshot currentBuildScene (no dictionary key lookup that can fail due to reconstructed references)

		EditorGUI.BeginChangeCheck();
		index = EditorGUILayout.Popup(index, dropdownButtons, GUILayout.Width(buttonWidth));

		if (EditorGUI.EndChangeCheck())
		{
			List<EditorBuildSettingsScene> scenes = EditorBuildSettings.scenes.ToList();
			EditorBuildSettingsScene existingEBSScene = scenes.Find(x => x.guid.ToString() == obj.GUID || x.path == obj.Path);//Find existing scene entry by GUID/path (stable identity)

			switch (index)
			{
				case 0: //Remove the item from BuildProfiles
					for (int i = scenes.Count - 1; i >= 0; i--)
					{
						if (scenes[i].guid.ToString() == obj.GUID || scenes[i].path == obj.Path)
						{
							scenes.RemoveAt(i);
						}
					}
					break;
				case 1: //Enable the scene in BuildProfiles
					if (existingEBSScene == null)
					{
						scenes.Add(new EditorBuildSettingsScene(obj.Path, true));
					}
					else
					{
						existingEBSScene.enabled = true;
					}
					break;
				case 2: //Disabled the scene in BuildProfiles
					if (existingEBSScene == null)
					{
						scenes.Add(new EditorBuildSettingsScene(obj.Path, false));
					}
					else
					{
						existingEBSScene.enabled = false;
					}
					break;
			}

			EditorBuildSettings.scenes = scenes.ToArray();

			GetSceneAssetsInDirectoryAndBuildSettings(); //Rebuild backing list/dictionary after we've updated build settings
		}
	}

	private void DrawSceneAssetField(UnityEngine.Object obj)
	{
		GUI.enabled = false;
		EditorGUILayout.ObjectField(obj, typeof(SceneAsset), false);
		GUI.enabled = true;
	}

	/// <summary>
	/// Button to Open a new scene file
	/// </summary>
	/// <param name="obj"></param>
	private void DrawSceneOpenButton(UnityEngine.Object obj)
	{
		float buttonHeight = EditorGUIUtility.singleLineHeight + 2f;
		const float buttonWidth = 32f;

		bool isOpen = EditorSceneManager.GetSceneByName(obj.name).isLoaded;
		string icon = isOpen ? "P4_CheckOutRemote@2x" :  "P4_CheckOutLocal@2x";

		GUI.enabled = !isOpen && !Application.isPlaying;
		GUIContent content = EditorGUIUtility.IconContent(icon, "Load this Scene (Closes all currently opened scenes)");
		GUIStyle style = EditorStyles.toolbarButton;
		if (GUILayout.Button(content, style, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
		{
			if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(obj), OpenSceneMode.Single);
			}
		}

		GUI.enabled = true;
	}

	/// <summary>
	/// Button to Add a scene to the current view
	/// </summary>
	/// <param name="obj"></param>
	private void DrawSceneAddButton(UnityEngine.Object obj)
	{
		float buttonHeight = EditorGUIUtility.singleLineHeight + 2f;
		const float buttonWidth = 32f;

		bool isOpen = EditorSceneManager.GetSceneByName(obj.name).isLoaded;

		if (isOpen)
		{
			bool onlyOpenScene = EditorSceneManager.sceneCount == 1; //if there is only a single unity scene open, then we need to disallow it from being closed (it's not possible anyways, but the UI should reflect this)
			if (Application.isPlaying)
			{
				GUI.enabled = false;
			}
			else
			{
				GUI.enabled = onlyOpenScene ? false : true;
			}
			
			string openIcon = onlyOpenScene ? "P4_LockedLocal@2x" : "P4_DeletedLocal@2x";

			GUIContent content = EditorGUIUtility.IconContent(openIcon, "Remove this scene from the current SceneView");
			GUIStyle style = EditorStyles.toolbarButton;
			if (GUILayout.Button(content, style, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
			{
				if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByName(obj.name), true);
				}	
			}
		}
		else
		{
			GUI.enabled = !Application.isPlaying;
			string addIcon = "P4_AddedRemote@2x";
			GUIContent content = EditorGUIUtility.IconContent(addIcon, "Add this Scene to the current SceneView (Additive)");
			GUIStyle style = EditorStyles.toolbarButton;
			if (GUILayout.Button(content, style, GUILayout.Height(buttonHeight), GUILayout.Width(buttonWidth)))
			{
				EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(obj), OpenSceneMode.Additive);
			}
		}

		GUI.enabled = true;

	}
}
