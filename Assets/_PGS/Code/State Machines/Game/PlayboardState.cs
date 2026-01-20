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
			throw new System.NotImplementedException();
		}

		public override async UniTask Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
