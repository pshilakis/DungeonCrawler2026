using PGS.Utilities;
using System;
using UnityEngine;

namespace PGS
{
	public class GameManager : Singleton<GameManager>
	{
		[ColoredHeader("Directory References", "#cc00ff", 14, true)]
		[SerializeField] private MapDirectory m_MapDirectory;
		public InputRelay InputRelay { get; private set; }
		
		[ColoredHeader("Character Prefab References", "#00ccff", 14, true)]
		[SerializeField] private Character characterPrefab;

		[ColoredHeader("Game State Machine (GSM)", "#ffff55", 14, true)]
		[SerializeField] private GameStateMachine m_StateMachine;

		[SerializeField] private string m_ActiveGameID;

		protected override async void Awake()
		{
			base.Awake();
			BootState.OnInputRelayInitialized += GetInputRelay;
			await m_MapDirectory.BuildDirectory();
			await m_StateMachine.Initialize();
		}

		private void GetInputRelay(InputRelay relay)
		{
			InputRelay = relay;
			BootState.OnInputRelayInitialized -= GetInputRelay;
		}

		public void SetCurrentMap(MapData mapData)
		{
			if (mapData == null) { return; }
			//if (m_CurrentMap.Data.ID == mapData.ID) { return; } //We already have loaded this map
			//m_CurrentMap = mapData.InstantiateMap();
		}
    }
}
