using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override async UniTask Enter()
		{
			await SceneUtilities.LoadScenes(requiredScenes, cts.Token);
		}

		public override async UniTask Exit()
		{
			
		}
	}
}
