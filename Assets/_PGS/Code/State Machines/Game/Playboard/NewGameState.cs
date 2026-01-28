using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PGS
{

	public class NewGameState : MonoBehaviour, IState
	{
		

		public async UniTask Enter()
		{
			Debug.Log("New Game State Enter()");
		}

		public UniTask Exit()
		{
			throw new System.NotImplementedException();
		}
	}
}
