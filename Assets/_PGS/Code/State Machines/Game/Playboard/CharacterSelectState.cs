using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{

	[System.Serializable]
	public class CharacterSelectState : IState
	{
		[SerializeField] private SceneData[] m_ScenesToLoad;
		public CharacterSelectView View { get; private set; }

		private CharacterSelectState ClaimOwner(GameView<CharacterSelectState> view)
		{
			View = view as CharacterSelectView;
			return this;
		}

		public async UniTask Enter()
		{
			CharacterSelectView.RequestOwner += ClaimOwner;
			await SceneUtilities.LoadScenes(m_ScenesToLoad);
		}

		public async UniTask Exit()
		{
			CharacterSelectView.RequestOwner -= ClaimOwner;
		}
	}
}
