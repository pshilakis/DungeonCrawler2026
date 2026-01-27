using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine;
using Cysharp.Threading.Tasks;
using System;

namespace PGS
{
	[System.Serializable]
    public class BootState : GameState
    {
		[Header("System Prefab References")]
		[SerializeField] private SceneLoader sceneLoaderPrefab;
		[SerializeField] private CinemachineBrain cameraPrefab;
		[SerializeField] private EventSystem eventSystemPrefab;

		public override bool RequireLoadScreenOnEnter => false;

		public static Action<InputRelay> OnInputRelayInitialized;

		public override async UniTask Enter()
		{
			InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			SetInputRelay(CommonUtilities.AddComponentToNewGameObject<InputRelay>(this.transform, nameof(InputRelay))); //Setup Input
			InstantiateSystemPrefab(cameraPrefab);
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
		}

		public override async UniTask Exit()
		{
			this.gameObject.SetActive(false);
		}


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
			Debug.Log("Input Initialized", this);
		}
	}
}
