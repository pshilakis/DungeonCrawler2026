using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using static PGS.SceneReference;
using Cysharp.Threading.Tasks;
using System.Threading;

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
			m_SceneName = asset.name;
			m_ScenePath = AssetDatabase.GetAssetPath(asset);
			AssetDatabase.TryGetGUIDAndLocalFileIdentifier<SceneAsset>(asset, out m_SceneGUID, out long file);
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

		[ReadOnly][SerializeField] private string m_SceneName;
		public string SceneName { get { return m_SceneName; } }

		[ReadOnly][SerializeField] private string m_SceneGUID;
		public string SceneGUID { get { return m_SceneGUID; } }

		[ReadOnly][SerializeField] private string m_ScenePath;
		public string Path { get { return m_ScenePath; } }

		[HideInInspector]public Scene scene;
		[ReadOnly][SerializeField] private SceneStatus status;

		public SceneStatus Status { get { return status; } }

		public void SetStatus(SceneStatus status)
		{
			this.status = status;
		}

		/// <summary>
		/// For troubleshooting issues with scenes since they can be weird
		/// </summary>
		public void DebugSceneReference()
		{
			string message = $"[SCENEREF DEBUG]\n";
			message += $"> name: {m_SceneName}\n";
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
		[SerializeField] private SceneAsset m_SceneAsset;
		[SerializeField] [Multiline] private string m_Description;

		[SerializeField] private SceneReference m_SceneRef;
		public string SceneName { get { return m_SceneRef.SceneName; }  }

		#region Serialization
		public void OnBeforeSerialize()
		{
			SerializeSceneData();
		}

		public void OnAfterDeserialize() { } //Not needed

		private void SerializeSceneData()
		{
			if (m_SceneAsset == null)
			{
				m_SceneRef = null;
			}
			else
			{
				m_SceneRef = CreateSceneReference(m_SceneAsset);
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
		#endregion

		public bool IsLoaded()
		{
			return m_SceneRef.Status == SceneStatus.LOADED;
		}

		public bool CanBeLoaded()
		{
			return !IsLoaded() && m_SceneRef.Status == SceneStatus.UNLOADED;
		}

		public async UniTask Load(LoadSceneMode mode, CancellationToken ct = default)
		{
			if (CanBeLoaded())
			{
				//ct.ThrowIfCancellationRequested();
				m_SceneRef.SetStatus(SceneStatus.LOADING);
				await SceneManager.LoadSceneAsync(m_SceneRef.SceneName, mode);
				m_SceneRef.SetStatus(SceneStatus.LOADED);
			}
		}

		public async UniTask Unload(CancellationToken ct = default)
		{
			if (IsLoaded())
			{
				//ct.ThrowIfCancellationRequested();
				m_SceneRef.SetStatus(SceneStatus.UNLOADING);
				await SceneManager.UnloadSceneAsync(m_SceneRef.SceneName);
				m_SceneRef.SetStatus(SceneStatus.UNLOADED);
			}
		}
	}
}
