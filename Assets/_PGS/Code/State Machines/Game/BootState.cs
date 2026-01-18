using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;
using Unity.Cinemachine;

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

		public override bool Enter()
		{
			OnEnter?.Invoke();
			InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			InstantiateSystemPrefab(cameraPrefab);
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
			GameManager.Instance.SetInputRelay(CommonUtilities.AddComponentToNewGameObject<InputRelay>(GameManager.Instance.transform, nameof(InputRelay))); //Setup Input
			OnEnterComplete?.Invoke();
			return true;
		}

		public override bool Exit()
		{
			OnExit?.Invoke();
			OnExitComplete?.Invoke();
			return true;
		}


		private void InstantiateSystemPrefab(Component component)
		{
			GameObject obj = GameObject.Instantiate(component.gameObject);
			obj.name = $"> {component.GetType().Name}";
			CommonUtilities.SetNewParent(obj, GameManager.Instance.transform);
		}
	}
}
