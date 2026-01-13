using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class LobbyState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override Task Enter()
		{
			OnStateEnter?.Invoke();
			SceneUtilities.LoadScenes(requiredScenes);
			//enable lobby UI input actionmap

			OnStateInitialized?.Invoke();
			return Task.CompletedTask;
		}

		public override Task Exit()
		{
			OnStateExitState?.Invoke();
			OnStateExitComplete?.Invoke();
			return Task.CompletedTask;
		}
	}
}
