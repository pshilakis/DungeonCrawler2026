using PGS.Utilities;
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

		protected override async void Awake()
		{
			base.Awake();
			SetInputRelay(CommonUtilities.AddComponentToNewGameObject<InputRelay>(this.transform, nameof(InputRelay))); //Setup Input
			await m_StateMachine.Initialize();
		}

		public void SetInputRelay(InputRelay relay)
		{
			InputRelay = relay;
			InputRelay.InitializeInput();
		}

		public void SetCurrentMap(MapData mapData)
		{
			if (mapData == null) { return; }
			//if (m_CurrentMap.Data.ID == mapData.ID) { return; } //We already have loaded this map
			//m_CurrentMap = mapData.InstantiateMap();
		}
    }
}
