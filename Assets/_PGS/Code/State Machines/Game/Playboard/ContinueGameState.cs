using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	public class ContinueGameState : MonoBehaviour, IState
	{
		public async UniTask Enter()
		{
			Debug.Log("ContinueGameState Enter()");
		}

		public async UniTask Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
