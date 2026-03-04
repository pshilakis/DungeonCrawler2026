using Animancer;
using Cysharp.Threading.Tasks;
using PGS.Utilities;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState, IState, IControlInput, IRequireLoadScreen
	{
		private SceneStateManager<LobbyState, LobbySceneState> m_SceneManager;

		public static Action<MapData> OnNewGameButtonPressed; //Passes the desired MapData when triggered
		public static Action OnContinueButtonPressed;

		#region IRequireLoadScreen
		public ClipTransition CustomIntro { get { return m_CustomIntro; } }

		public ClipTransition CustomOutro { get { return m_CustomOutro; } }
		#endregion

		#region Unity Lifecycle
		private void Awake()
		{
			LobbySceneStateManager.OnSceneManagerOwnerRequest += RegisterSceneManager;
		}

		private void OnDestroy()
		{
			LobbySceneStateManager.OnSceneManagerOwnerRequest -= RegisterSceneManager;
		}
		#endregion

		#region IState
		public override async UniTask Enter()
		{
			//LobbyViewManager.OnInitialize += RegisterManagerToState;
			await SceneUtilities.LoadScenes(requiredScenes);

			//instantiate the buttons for new game and any saved games
			string[] savedGames = RuntimeSaveUtilities.GetListOfSavedGameIDs();

			foreach (string gameID in savedGames)
			{
				Debug.Log(gameID);
			}
		}

		public override async UniTask Exit()
		{
			
			gameObject.SetActive(false);
		}
		#endregion

		#region IControlInput
		public void EnableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap
		}

		public void DisableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
		}
		#endregion

		private void RegisterSceneManager(SceneStateManager<LobbyState, LobbySceneState> manager)
		{
			m_SceneManager = manager;

			//Subscribe to scenemanager events
		}
	}
}
