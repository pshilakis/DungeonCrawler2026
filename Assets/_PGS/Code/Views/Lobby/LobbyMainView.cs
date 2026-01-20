using UnityEngine;
using PGS.UI;

namespace PGS
{
    public class LobbyMainView : GameView
    {
        [SerializeField] private ButtonHandler newGameButton;
        [SerializeField] private ButtonHandler continueGameButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			newGameButton.OnPress += LoadNewGame;
			continueGameButton.OnPress += LoadExistingGame;
		}

		protected override void OnDisable()
		{
			newGameButton.OnPress -= LoadNewGame;
			continueGameButton.OnPress -= LoadExistingGame;
			base.OnDisable();
		}

		private void LoadNewGame()
		{
			LobbyState.OnNewGameButtonPressed?.Invoke();
		}

		private void LoadExistingGame()
		{
			LobbyState.OnContinueButtonPressed?.Invoke();
		}


	}
}
