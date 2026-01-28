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

		[Header("Character Select/Player Setup Scene")]
		[Tooltip("In case we need to load new game elements, we can load them into this scene and then unload the scene to remove their references entirely. Maybe that's too complicated, idk.")]
		[SerializeField] private SceneData newGameScene;

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
			await SceneUtilities.ShowLoadScreen(true);
			await SceneUtilities.LoadSceneAdditive(newGameScene);
			//If completely new game with no character data, load the CharacterSelectView
			await m_Substates.SetState(m_CharacterSelect);
			//else if we have character data (maybe we crashed after making them? or else we want to append characters?) load CharacterSelect from data
			//else if we have everything we need, and we can set player turns
			//await m_Substates.SetState(m_PlayerTurnSelect);
			//else we're done and can load the next state

			await SceneUtilities.HideLoadScreen(true);
		}

		public async UniTask Exit()
		{
			await m_Substates.CurrentState.Exit();

			if (newGameScene.IsLoaded())
			{
				await SceneUtilities.UnloadScene(newGameScene);
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
