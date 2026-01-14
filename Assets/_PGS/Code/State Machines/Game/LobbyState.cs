using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override bool Enter()
		{
			OnStateEnter?.Invoke();
			SceneUtilities.LoadScenes(requiredScenes);
			GameManager.Instance.InputRelay.Input.Controls.UI.Enable(); //Enable lobby UI input actionmap

			OnStateInitialized?.Invoke();
			return true;
		}

		public override bool Exit()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable(); //Enable lobby UI input actionmap
			OnStateExitState?.Invoke();
			OnStateExitComplete?.Invoke();
			return true;
		}
	}
}
