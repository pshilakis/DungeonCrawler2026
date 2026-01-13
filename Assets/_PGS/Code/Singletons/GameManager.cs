using PGS.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PGS
{
	public class GameManager : Singleton<GameManager>
	{
		[ColoredHeader("Game State Machine (GSM)", "#ffff55", 14, true)]
		[SerializeField] private GameStateMachine m_StateMachine;
		
		[ColoredHeader("Character Prefab References", "#00ccff", 14, true)]
		[SerializeField] private Character characterPrefab;
		[SerializeField] private Character[] activeCharacters;

		protected override void Awake()
		{
			base.Awake();
			m_StateMachine.Initialize();
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
