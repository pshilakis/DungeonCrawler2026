using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PGS
{
	public class GameManager : Singleton<GameManager>
	{
		//[ColoredHeader("Scene References", 14, true)]
		//[SerializeField] private SceneData lobbyScene;
		//[SerializeField] private SceneData[] testSceneLoads;

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
				GSM.Initialize();
			}
			else
			{
				Debug.LogError("NO GAME STATE MACHINE FOUND");
				//Close the game
			}
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
