using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayerTurnSelectState : IState
	{
		[SerializeField] private PlayerTurnSelectView m_ViewPrefab;
		public PlayerTurnSelectView View { get; private set; }

		public async UniTask Enter()
		{
			View = GameObject.Instantiate(m_ViewPrefab);
		}

		public async UniTask Exit()
		{

		}
	}
}
