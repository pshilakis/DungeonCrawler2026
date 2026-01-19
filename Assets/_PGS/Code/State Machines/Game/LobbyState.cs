using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override async Task Enter()
		{
			Debug.Log($"{this.GetType()} ENTER START @ {DateTime.Now}");
			//OnStateEnter?.Invoke();
			SceneUtilities.LoadScenes(requiredScenes);
			//GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap
			Debug.Log($"{this.GetType()} ENTER COMPLETE @ {DateTime.Now}");
			//OnStateInitialized?.Invoke();
		}

		public override async Task Exit()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
			//OnStateExitState?.Invoke();
			//OnStateExitComplete?.Invoke();
		}
	}
}
