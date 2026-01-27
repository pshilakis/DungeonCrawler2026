using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState, IState, IControlInput
	{
		public override bool RequireLoadScreenOnEnter => true;

		[ReadOnly][SerializeField] private LobbyViewManager m_ViewManager;

		public static Action OnNewGameButtonPressed;
		public static Action OnContinueButtonPressed;

		public override async UniTask Enter()
		{
			LobbyViewManager.OnInitialize += RegisterManagerToState;
			await SceneUtilities.LoadScenes(requiredScenes);
		}

		public override async UniTask Exit()
		{
			LobbyViewManager.OnInitialize -= RegisterManagerToState;
			gameObject.SetActive(false);
		}

		private void RegisterManagerToState(GameViewManager manager)
		{
			if (manager is LobbyViewManager)
			{
				m_ViewManager = manager as LobbyViewManager;
			}
		}

		public void EnableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap
		}

		public void DisableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
		}
	}
}
