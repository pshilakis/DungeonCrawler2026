using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState, IControlInput
	{
		private StateMachine<IState> m_States = new StateMachine<IState>();
		[Header("Substates")]
		[SerializeField] private StartGameState startGameState;
		[SerializeField] private PlayGameState playGameState;

		[Header("Game Info")]
		[SerializeField] private Character[] activeCharacters;
		[ReadOnly][SerializeField] private MapManager m_CurrentMapManager;

		private bool m_IsDraggingCamera = false;

		public override bool RequireLoadScreenOnEnter => true;

		private void Awake()
		{
			startGameState.OnParentStateRequest += () => this;
			playGameState.OnParentStateRequest += () => this;

			MapManager.OnManagerLoaded += SetCurrentMapManager;

		}

		private void OnDestroy()
		{
			MapManager.OnManagerLoaded -= SetCurrentMapManager;
		}

		public override async UniTask Enter()
		{
			await SceneUtilities.LoadScenes(requiredScenes);
			//Determine whether we're in a new game or a loaded existing game, and then set the correct substate
		}

		public override async UniTask Exit()
		{
			await m_States.CurrentState.Exit();
			gameObject.SetActive(false);
		}

		private PlayboardState SetCurrentMapManager(MapManager manager)
		{
			m_CurrentMapManager = manager;
			return this;
		}

		public void EnableInputs()
		{
			if (m_States.CurrentState == null) { return; }
			if (m_States.CurrentState is IControlInput)
			{
				IControlInput input = m_States.CurrentState as IControlInput;
				input.EnableInputs();
			}
		}

		public void DisableInputs()
		{
			if (m_States.CurrentState == null) { return; }
			if (m_States.CurrentState is IControlInput)
			{
				IControlInput input = m_States.CurrentState as IControlInput;
				input.DisableInputs();
			}
		}
	}
}
