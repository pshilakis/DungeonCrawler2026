using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class BoardState : GameState
	{
		public override bool RequireLoadScreenOnEnter => true;

		public override bool Enter()
		{
			throw new System.NotImplementedException();
		}

		public override bool Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
