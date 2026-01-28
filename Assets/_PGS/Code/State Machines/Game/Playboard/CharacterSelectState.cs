using Cysharp.Threading.Tasks;
using PGS.Utilities;
using UnityEngine;

namespace PGS
{

	[System.Serializable]
	public class CharacterSelectState : IState
	{
		[SerializeField] private CharacterSelectView m_ViewPrefab;
		public CharacterSelectView View { get; private set; }

		public async UniTask Enter()
		{
			View = GameObject.Instantiate(m_ViewPrefab);
		}

		public async UniTask Exit()
		{

		}
	}
}
