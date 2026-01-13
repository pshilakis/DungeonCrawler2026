using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class BoardState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override Task Enter()
		{
			throw new System.NotImplementedException();
		}

		public override Task Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
