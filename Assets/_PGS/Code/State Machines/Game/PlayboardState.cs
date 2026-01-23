using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState
	{
		[SerializeField] private Character[] activeCharacters;

		[SerializeField] private NewGameState newGameState;
		[SerializeField] private PlayboardSubstate characterCreationState;

		private StateMachine<IState> m_States = new StateMachine<IState>();

		//required states
		//New Game start
		//> Character customization
		//> Select player order
		//Continue Game start
		//> load save data
		//Player Roll State
		//Player Move state
		//Player reward state


		public override bool RequireLoadScreenOnEnter => true;

		public override async UniTask Enter()
		{
			await SceneUtilities.LoadScenes(requiredScenes);
		}

		public override async UniTask Exit()
		{
			gameObject.SetActive(false);
		}
	}
}
