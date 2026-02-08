using Cysharp.Threading.Tasks;
using PGS.Utilities;
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

		//[Header("Character Select/Player Setup Scene")]
		//[Tooltip("In case we need to load new game elements, we can load them into this scene and then unload the scene to remove their references entirely. Maybe that's too complicated, idk.")]
		//[SerializeField] private SceneData newGameScene;

		[Header("Substates")]
		[SerializeField] private CharacterSelectState m_CharacterSelect;
		[SerializeField] private PlayerTurnSelectState m_PlayerTurnSelect;

		[SerializeField] private StateMachine<IState> m_Substates = new StateMachine<IState>();


		//Substates
		//New Game
			//Character Selection
			//Set Player Turn order
			//Create new game save data?

		//Continue Game
			//Load existing game save data

		#region Events
		public Func<PlayboardState> OnParentStateRequest;
		//Event to request current game data

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
			await ChooseInitialSubstate();
		}

		public async UniTask Exit()
		{
			await m_Substates.CurrentState.Exit();
			gameObject.SetActive(false);
		}
		#endregion

		private async UniTask ChooseInitialSubstate()
		{
			//SUBSTATE SELECT LOGIC:
			//If completely new game with no character data, load the CharacterSelectView
			await LoadCharacterSelect();
			//else if we have character data (maybe we crashed after making them? or else we want to append characters?) load CharacterSelect from data
			//else if we have everything we need, and we can set player turns
			//await LoadTurnSelect();
			//else we're done and can load the next state
		}

		private async UniTask LoadCharacterSelect()
		{
			await SceneUtilities.ShowLoadScreen(true);
			await m_Substates.SetState(m_CharacterSelect);
			m_CharacterSelect.View.PlayButton.OnPress += UniTask.Action(async () => { LoadTurnSelect(); });
			await SceneUtilities.HideLoadScreen(true);
		}

		private async UniTask LoadTurnSelect()
		{
			await SceneUtilities.ShowLoadScreen(true);
			await m_Substates.SetState(m_PlayerTurnSelect);
			//subscribe to play button to send to parent and begin gameplay
			await SceneUtilities.HideLoadScreen(true);
		}

		#region Unity Lifecycle
		private void Awake()
		{
			m_Parent = OnParentStateRequest?.Invoke();
		}
		#endregion
	}
}
