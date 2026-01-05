using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PGS
{
    public class GameManager : Singleton<GameManager>
    {
		[ColoredHeader("System Prefab References", 14, true)]
		[SerializeField] private SceneLoader sceneLoaderPrefab;
		[SerializeField] private EventSystem eventSystemPrefab;

		protected override void Awake()
		{
			base.Awake();

			InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
			CommonUtilities.AddComponentToNewGameObject<InputRelay>(this.transform, nameof(InputRelay)); //Setup Input

		}

		private void InstantiateSystemPrefab(Component component)
		{
			GameObject obj = GameObject.Instantiate(component.gameObject);
			obj.name = $"> {component.GetType().Name}";
			CommonUtilities.SetNewParent(obj, this.transform);
		}
    }
}
