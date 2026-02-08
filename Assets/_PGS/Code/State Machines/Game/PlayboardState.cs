using Animancer;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState, IControlInput, IRequireLoadScreen
	{
		[Header("Substates")]
		[SerializeField] private StartGameState startGameState;
		[SerializeField] private PlayGameState playGameState;
		private StateMachine<IState> m_Substates = new StateMachine<IState>();

		#region IControlInput
		public void EnableInputs()
		{
			if (m_Substates.CurrentState == null) { return; }
			if (m_Substates.CurrentState is IControlInput)
			{
				IControlInput input = m_Substates.CurrentState as IControlInput;
				input.EnableInputs();
			}
		}

		public void DisableInputs()
		{
			if (m_Substates.CurrentState == null) { return; }
			if (m_Substates.CurrentState is IControlInput)
			{
				IControlInput input = m_Substates.CurrentState as IControlInput;
				input.DisableInputs();
			}
		}
		#endregion

		#region IRequireLoadScreen
		public ClipTransition CustomIntro { get { return m_CustomIntro; } }

		public ClipTransition CustomOutro { get { return m_CustomOutro; } }
		#endregion

		private void Awake()
		{
			startGameState.OnParentStateRequest += () => this;
			playGameState.OnParentStateRequest += () => this;
		}

		#region IState
		public override async UniTask Enter()
		{
			startGameState.OnGameReady += PlayGame;

			await SceneUtilities.LoadScenes(requiredScenes); //Load base scene

			//Determine whether we're in a new game or a loaded existing game, and then set the correct substate
			TryContinueExistingGame(null);
			await m_Substates.SetState(startGameState);
		}

		private async void PlayGame()
		{
			startGameState.OnGameReady -= PlayGame;
			await m_Substates.SetState(playGameState);
		}

		public override async UniTask Exit()
		{
			await m_Substates.CurrentState.Exit();
			gameObject.SetActive(false);
		}
		#endregion

		public void SetMapData(MapData data)
		{
			playGameState.SetMapData(data);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id">The saveID of the game we want to load. If null, we're starting a new game.</param>
		public void TryContinueExistingGame(string id)
		{
			
		}


	}
}
