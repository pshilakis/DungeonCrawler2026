using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;
using System;

namespace PGS
{
	/// <summary>
	/// BootState handles setting up all core game services before loading into the first actual scene
	/// </summary>
	[System.Serializable]
    public class BootState : GameState
    {
		[Header("System Prefab References")]
		[SerializeField] private SceneLoader sceneLoaderPrefab;
		[SerializeField] private CinemachineBrain cameraPrefab;
		[SerializeField] private EventSystem eventSystemPrefab;

		public static Action<InputRelay> OnInputRelayInitialized;

		#region IState
		public override async UniTask Enter()
		{
			//TODO: load user settings
			//TODO: init save/load functionality
			InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			SetInputRelay(CommonUtilities.AddComponentToNewGameObject<InputRelay>(GameManager.Instance.transform, nameof(InputRelay))); //Setup Input
			InstantiateSystemPrefab(cameraPrefab);
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
		}

		public override async UniTask Exit()
		{
			this.gameObject.SetActive(false);
		}
		#endregion

		private void InstantiateSystemPrefab(Component component)
		{
			GameObject obj = GameObject.Instantiate(component.gameObject);
			obj.name = $"> {component.GetType().Name}";
			CommonUtilities.SetNewParent(obj, GameManager.Instance.transform);
		}

		public void SetInputRelay(InputRelay relay)
		{
			relay.InitializeInput();
			OnInputRelayInitialized?.Invoke(relay);
		}
	}
}
