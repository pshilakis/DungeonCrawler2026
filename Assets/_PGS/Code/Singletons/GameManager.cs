using System;
using UnityEngine;

namespace PGS
{
	public class GameManager : Singleton<GameManager>
	{
		[ColoredHeader("Directory References", "#cc00ff", 14, true)]
		[SerializeField] private MapDirectory m_MapDirectory;
		public InputRelay InputRelay { get; private set; }

		[ColoredHeader("Game State Machine (GSM)", "#ffff55", 14, true)]
		[SerializeField] private GameStateMachine m_StateMachine;

		public CharacterManager Characters { get; private set; }

		[SerializeField] private string m_ActiveGameID;
		public string GameID {
			get { return m_ActiveGameID; }
			private set {  m_ActiveGameID = value; }
		}

		protected override async void Awake()
		{
			base.Awake();
			GameID = Guid.NewGuid().ToString();
			BootState.OnInputRelayInitialized += GetInputRelay;
			BootState.OnCharacterManagerLocated += GetCharacterManager;
			m_MapDirectory.BuildDirectory();
			m_StateMachine.Initialize();

		}

		private void GetInputRelay(InputRelay relay)
		{
			InputRelay = relay;
			BootState.OnInputRelayInitialized -= GetInputRelay;
		}

		private void GetCharacterManager(CharacterManager charManager)
		{
			Characters = charManager;
			BootState.OnCharacterManagerLocated -= GetCharacterManager;
		}
    }
}
