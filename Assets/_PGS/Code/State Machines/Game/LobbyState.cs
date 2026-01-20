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

		public override async UniTask Enter()
		{
			await SceneUtilities.LoadScenes(requiredScenes, cts.Token);	
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap
		}

		public override async UniTask Exit()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
			//OnStateExitState?.Invoke();
			//OnStateExitComplete?.Invoke();
		}
	}
}
