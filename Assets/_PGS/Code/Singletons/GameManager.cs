using UnityEngine;

namespace PGS
{
	public class GameManager : Singleton<GameManager>
	{
		[ColoredHeader("Game State Machine (GSM)", "#ffff55", 14, true)]
		[SerializeField] private GameStateMachine m_StateMachine;

		public InputRelay InputRelay { get; private set; }
		
		[ColoredHeader("Character Prefab References", "#00ccff", 14, true)]
		[SerializeField] private Character characterPrefab;
		[SerializeField] private Character[] activeCharacters;

		protected override void Awake()
		{
			base.Awake();
			m_StateMachine.Initialize();
		}

		public void SetInputRelay(InputRelay relay)
		{
			InputRelay = relay;
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
