using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEditor;
using System.Linq;
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

	private List<EditorObjectReference> FoundScenes = new List<EditorObjectReference>();

    private void OnEnable()
    {
        GetSceneAssetsInDirectory(); //update the scenes dictionary
        EditorApplication.projectChanged += GetSceneAssetsInDirectory;
		EditorSceneManager.sceneOpened += Repaint;
		EditorSceneManager.sceneClosed += Repaint;
    }

	private void OnDisable()
    {
		EditorApplication.projectChanged -= GetSceneAssetsInDirectory;
		EditorSceneManager.sceneOpened -= Repaint;
		EditorSceneManager.sceneClosed -= Repaint;
	}

    protected override void OnGUI()
    {
		foreach(EditorObjectReference entry in FoundScenes)
		{
			if (Application.isPlaying)
			{
				GUI.enabled = false;
			}

			EditorGUILayout.BeginHorizontal();
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

	private void GetSceneAssetsInDirectory()
	{
		FoundScenes.Clear();

		string[] guids = AssetDatabase.FindAssets($"t:{typeof(SceneAsset).Name}", m_SearchDirectory); //Need the typeof(T).Name here otherwise typeof(SceneAsset) returns "UnityEditor.SceneAsset" which does not work with the filter; we need to remove the namespace

		foreach (string guid in guids)
		{
			FoundScenes.Add(new EditorObjectReference(guid));
		}

		FoundScenes = FoundScenes.OrderBy(x => x.Asset.name).ToList(); //Sort the list

		Debug.Log($"{FoundScenes.Count} SceneAssets Found in Directory \"{m_SearchDirectory[0]}\"");
	}

	private void DrawSceneAssetField(Object obj)
	{
		GUI.enabled = false;
		EditorGUILayout.ObjectField(obj, typeof(SceneAsset), false);
		GUI.enabled = true;
	}

	/// <summary>
	/// Button to Open a new scene file
	/// </summary>
	/// <param name="obj"></param>
	private void DrawSceneOpenButton(Object obj)
	{
		float buttonHeight = EditorGUIUtility.singleLineHeight + 2f;
		const float buttonWidth = 32f;

		bool isOpen = EditorSceneManager.GetSceneByName(obj.name).isLoaded;
		string icon = isOpen ? "P4_CheckOutRemote@2x" :  "P4_CheckOutLocal@2x";

		GUI.enabled = !isOpen && !Application.isPlaying;
		GUIContent content = EditorGUIUtility.IconContent(icon, "Load this Scene (Closes all currently opened scenes)"); ;
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
	private void DrawSceneAddButton(Object obj)
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
