using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override async Task Enter()
		{
			throw new System.NotImplementedException();
		}

		public override async Task Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
