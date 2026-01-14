using PGS.Utilities;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PGS
{
	[System.Serializable]
    public class BootState : GameState
    {
		[Header("System Prefab References")]
		[SerializeField] private SceneLoader sceneLoaderPrefab;
		[SerializeField] private EventSystem eventSystemPrefab;

		public override bool RequireLoadScreenOnEnter => false;

		public override bool Enter()
		{
			OnStateEnter?.Invoke();
			InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
			CommonUtilities.AddComponentToNewGameObject<InputRelay>(GameManager.Instance.transform, nameof(InputRelay)); //Setup Input
			OnStateInitialized?.Invoke();
			return true;
		}

		public override bool Exit()
		{
			OnStateExitState?.Invoke();
			OnStateExitComplete?.Invoke();
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
