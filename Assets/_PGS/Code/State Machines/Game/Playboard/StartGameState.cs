using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// The first state when we enter a game; either start a new game or continue an existing one
	/// </summary>
	[System.Serializable]
	public class StartGameState : MonoBehaviour, IState, IControlInput
	{
		private PlayboardState m_Parent;

		[Header("Substates")]
		[SerializeField] private NewGameState newGameState;
		[SerializeField] private ContinueGameState continueGameState;
		private StateMachine<IState> m_Substates = new StateMachine<IState>();

		//Substates
		//New Game
			//Character Selection
			//Set Player Turn order
			//Create new game save data?

		//Continue Game
			//Load existing game save data

		#region Events
		public Func<PlayboardState> OnParentStateRequest;

		/// <summary>
		/// Event for when the game has fully started and we're ready to move to the Play game state
		/// </summary>
		public Action OnGameReady;
		#endregion

		#region IControlInput
		public void EnableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable();
		}

		public void DisableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable();
		}
		#endregion

		#region IState
		public async UniTask Enter()
		{
			OnGameReady?.Invoke();
		}

		public async UniTask Exit()
		{
			if (m_Substates.CurrentState != null)
			{
				await m_Substates.CurrentState.Exit();
			}
			
			gameObject.SetActive(false);
		}
		#endregion

		private void Awake()
		{
			m_Parent = OnParentStateRequest?.Invoke();
		}

		public void SetParentState(PlayboardState parent)
		{
			m_Parent = parent;
		}
	}
}
