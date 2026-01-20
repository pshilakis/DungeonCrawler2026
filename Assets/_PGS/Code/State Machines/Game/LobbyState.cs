using Cysharp.Threading.Tasks;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		[ReadOnly][SerializeField] private LobbyViewManager m_ViewManager;

		public override async UniTask Enter()
		{
			LobbyViewManager.OnInitialize += RegisterManagerToState;
			await SceneUtilities.LoadScenes(requiredScenes, cts.Token);	
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap

		}

		private void RegisterManagerToState(GameViewManager manager)
		{
			Debug.Log("huh");
			if (manager is LobbyViewManager)
			{
				m_ViewManager = manager as LobbyViewManager;
			}
		}

		public override async UniTask Exit()
		{
			LobbyViewManager.OnInitialize -= RegisterManagerToState;
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
			//OnStateExitState?.Invoke();
			//OnStateExitComplete?.Invoke();

		}
	}
}
