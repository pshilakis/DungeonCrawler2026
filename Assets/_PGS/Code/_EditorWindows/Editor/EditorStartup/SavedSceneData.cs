using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace PGS
{
	
	[CreateAssetMenu(fileName = ASSET_NAME, menuName = "PGS/Editor/Editor Scene Save Data")]
	public class SavedSceneData : ScriptableObject
	{
		[System.Serializable]
		public class SavedScene
		{
			[HideInInspector] public string name;
			[ReadOnly] public string sceneGUID;
			[ReadOnly] public SceneAsset sceneAsset;
			[ReadOnly] public bool wasActiveScene;

			public SavedScene(string guid, bool activeScene)
			{
				string path = AssetDatabase.GUIDToAssetPath(guid);
				sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);
				name = sceneAsset.name;
				sceneGUID = guid;
				wasActiveScene = activeScene;
			}
		}

		private const string ASSET_NAME = "Editor Save Scene Data";
		public SceneAsset bootScene;
		[SerializeField] private List<SavedScene> sceneList = new List<SavedScene>();
		public List<SavedScene> SavedSceneList { get { return sceneList; } }
		public void AddGUID(string guid, bool activeScene)
		{
			sceneList.Add(new SavedScene(guid, activeScene));
		}

		public void ClearGUIDList()
		{
			sceneList.Clear();
		}
	}

	[CustomEditor(typeof(SavedSceneData))]
	public class SavedSceneDataEditor : UnityEditor.Editor
	{
		private string _guid;

		private void Awake()
		{
			AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this.serializedObject.targetObject, out _guid, out long file);
		}

		public override void OnInspectorGUI()
		{
			GUI.enabled = false;
			EditorGUILayout.TextField("GUID:", _guid);
			GUI.enabled = true;
			base.OnInspectorGUI();
		}
	}
}


