using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayerTurnSelectState : IState
	{
		[SerializeField] private SceneData[] m_ScenesToLoad;

		public PlayerTurnSelectView View { get; private set; }

		private PlayerTurnSelectState ClaimOwner(SceneState<PlayerTurnSelectState> view)
		{
			View = view as PlayerTurnSelectView;
			return this;
		}
		public async UniTask Enter()
		{
			PlayerTurnSelectView.RequestOwner += ClaimOwner;
			await SceneUtilities.LoadScenes(m_ScenesToLoad);
			await View.SetPlayerTurnCards(GameManager.Instance.Characters.CharacterDataList); //instantiate the player cards in the view

		}

		public async UniTask Exit()
		{
			PlayerTurnSelectView.RequestOwner -= ClaimOwner;
		}
	}
}
