using Animancer;
using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState, IState, IControlInput, IRequireLoadScreen
	{
		[ReadOnly][SerializeField] private LobbyViewManager m_ViewManager;

		public static Action<MapData> OnNewGameButtonPressed; //Passes the desired MapData when triggered
		public static Action OnContinueButtonPressed;

		#region IRequireLoadScreen
		public ClipTransition CustomIntro { get { return m_CustomIntro; } }

		public ClipTransition CustomOutro { get { return m_CustomOutro; } }
		#endregion

		#region IState
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
		#endregion

		private void RegisterManagerToState(GameViewManager manager)
		{
			if (manager is LobbyViewManager)
			{
				m_ViewManager = manager as LobbyViewManager;
			}
		}

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
	}
}
