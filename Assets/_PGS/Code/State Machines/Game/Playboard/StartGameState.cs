using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// The New Game state is entered when a user clicks the "New Game" button
	/// </summary>
	[System.Serializable]
	public class StartGameState : MonoBehaviour, IState, IControlInput
	{
		private PlayboardState m_Parent;
		//Substates
		//New Game
		//Character Selection
		//Set Player Turn order
		//Continue Game
		//Load game data

		#region Events
		public Func<PlayboardState> OnParentStateRequest;
		#endregion

		private void Awake()
		{
			m_Parent = OnParentStateRequest?.Invoke();
		}

		public async UniTask Enter()
		{
			
		}

		public async UniTask Exit()
		{
			gameObject.SetActive(false);
		}

		public void SetParentState(PlayboardState parent)
		{
			m_Parent = parent;
		}

		public void EnableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable();
		}

		public void DisableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable();
		}
	}
}
