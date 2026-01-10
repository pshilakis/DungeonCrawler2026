using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace PGS
{
	//https://github.com/Tymski/SceneReference/blob/master/Scripts/SceneReference.cs
	/// <summary>
	/// Holds Scene information and sends events based on scene state
	/// </summary>
	[System.Serializable]
	public class SceneReference
	{
		#region Constructor
		public SceneReference(SceneAsset asset)
		{
			_sceneName = asset.name;
			_scenePath = AssetDatabase.GetAssetPath(asset);
			AssetDatabase.TryGetGUIDAndLocalFileIdentifier<SceneAsset>(asset, out _sceneAssetGUID, out long file);
			status = SceneStatus.UNLOADED;
		}
		#endregion

		public enum SceneStatus
		{
			UNLOADED,
			LOADING,
			LOADED,
			UNLOADING
		}

		[ReadOnly] [SerializeField] private string _sceneName;
		public string SceneName { get { return _sceneName; } }

		[ReadOnly][SerializeField] private string _sceneAssetGUID;
		public string SceneGUID { get { return _sceneAssetGUID; } }

		[ReadOnly][SerializeField] private string _scenePath;
		public string Path { get { return _scenePath; } }

		[ReadOnly] public Scene scene;
		[ReadOnly] public SceneStatus status;
		//public SceneStatus Status { get { return status; } }

		public bool IsLoaded { get { return scene.isLoaded; } }


		//#region Events
		//public delegate void SceneEvent(SceneReference sceneRef, Scene? scene = null);
		//public event SceneEvent OnSceneLoading;
		//public event SceneEvent OnSceneLoaded;
		//public event SceneEvent OnSceneUnloading;
		//public event SceneEvent OnSceneUnloaded;

		//public void SetLoading()
		//{
		//	status = SceneStatus.LOADING;
		//	OnSceneLoading?.Invoke(this);
		//}

		//public virtual void SetLoaded(bool setActive = true)
		//{
		//	status = SceneStatus.LOADED;
		//	scene = SceneManager.GetSceneByName(_sceneName);
		//	if (setActive)
		//	{
		//		SceneManager.SetActiveScene(scene);
		//	}

		//	OnSceneLoaded?.Invoke(this, scene);
		//}

		//public void SetUnloading()
		//{
		//	status = SceneStatus.UNLOADING;
		//	OnSceneUnloading?.Invoke(this);
		//}

		//public virtual void SetUnloaded()
		//{
		//	status = SceneStatus.UNLOADED;
		//	OnSceneUnloaded?.Invoke(this);
		//}
		//#endregion

		//public bool IsLoading()
		//{
		//	return status == SceneStatus.LOADING;
		//}

		//public bool IsLoaded()
		//{
		//	return status == SceneStatus.LOADED;
		//}

		//public bool IsUnloading()
		//{
		//	return status == SceneStatus.UNLOADING;
		//}

		//public bool IsUnloaded()
		//{
		//	return status == SceneStatus.UNLOADED;
		//}

		/// <summary>
		/// For troubleshooting issues with scenes since they can be weird
		/// </summary>
		public void DebugSceneReference()
		{
			string message = $"[SCENEREF DEBUG]\n";
			message += $"> name: {_sceneName}\n";
			message += $"> status: {status}\n";
			message += $"> scene: {scene}\n";
			message += $"> scene.path: {scene.path}\n";
			message += $"> scene.buildIndex: {scene.buildIndex}\n";
			message += $"> scene.isValid(): {scene.IsValid()}\n";
			message += $"> scene.isLoaded: {scene.isLoaded}\n";
			Debug.Log(message);
		}
	}

	[CreateAssetMenu(fileName = "New SceneData", menuName = "PGS/Scriptable Objects/Scenes/Scene Data")]
	public class SceneData : ScriptableObject, ISerializationCallbackReceiver
	{
		[SerializeField] private SceneAsset sceneAsset;
		[SerializeField] [Multiline] private string _sceneDescription;

		[SerializeField] private SceneReference _sceneRef;
		public SceneReference SceneRef
		{
			get { return _sceneRef; }
			private set { _sceneRef = value; }
		}

		public void OnBeforeSerialize()
		{
			SerializeSceneData();
		}

		public void OnAfterDeserialize() { } //Not needed

		private void SerializeSceneData()
		{
			if (sceneAsset == null)
			{
				SceneRef = null;
			}
			else
			{
				SceneRef = CreateSceneReference(sceneAsset);
			}
		}

		private SceneReference CreateSceneReference(SceneAsset asset)
		{
			if (asset == null)
			{
				if (Application.isPlaying)
				{
					Debug.LogError($"Required SceneAsset has not been set in {nameof(SceneData)}", this);
				}

				return null;
			}

			return new SceneReference(asset);
		}

		public bool ValidateSceneFiles()
		{
			//if (StartupScene == null)
			//{
			//	Debug.LogWarning($"SCENE LOAD ERROR: STARTUP SCENE NOT FOUND!");
			//	return false;
			//}
			
			//if (LoadingScene == null)
			//{
			//	Debug.LogWarning($"SCENE LOAD ERROR: LOBBY SCENE NOT FOUND!");
			//	return false;
			//}
			
			//if (GameScene == null)
			//{
			//	Debug.LogWarning($"SCENE LOAD ERROR: GAME SCENE NOT FOUND!");
			//	return false;
			//}

			return true;
		}
	}
}
