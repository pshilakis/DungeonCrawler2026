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
			//enable lobby UI input actionmap

			OnStateInitialized?.Invoke();
			return true;
		}

		public override bool Exit()
		{
			OnStateExitState?.Invoke();
			OnStateExitComplete?.Invoke();
			return true;
		}
	}
}
