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

		[ColoredHeader("Scene References", 14, true)]
		[SerializeField] private SceneData lobbyScene;
		[SerializeField] private SceneData[] testSceneLoads;

		[ColoredHeader("Character Prefab References", 14, true)]
		[SerializeField] private Character characterPrefab;
		[SerializeField] private Character[] activeCharacters;

		private GameStateMachine m_GSM;
		public GameStateMachine GSM { get { return m_GSM; } }
		

		protected override void Awake()
		{
			base.Awake();

			if (CommonUtilities.TryGetComponentInChildren<GameStateMachine>(this, out m_GSM))
			{
				
			}
			else
			{
				Debug.LogError("NO GAME STATE MACHINE FOUND");
			}

				InstantiateSystemPrefab(sceneLoaderPrefab); //Setup SceneLoader
			InstantiateSystemPrefab(eventSystemPrefab); //Setup EventSystem for UI Input
			CommonUtilities.AddComponentToNewGameObject<InputRelay>(this.transform, nameof(InputRelay)); //Setup Input
			SceneUtilities.LoadScenes(testSceneLoads);
		}

		private void InstantiateSystemPrefab(Component component)
		{
			GameObject obj = GameObject.Instantiate(component.gameObject);
			obj.name = $"> {component.GetType().Name}";
			CommonUtilities.SetNewParent(obj, this.transform);
		}

		private void InstantiatePlayers()
		{

		}

		public void SetCurrentMap(MapData mapData)
		{
			if (mapData == null) { return; }
			//if (m_CurrentMap.Data.ID == mapData.ID) { return; } //We already have loaded this map
			//m_CurrentMap = mapData.InstantiateMap();
		}
    }
}
