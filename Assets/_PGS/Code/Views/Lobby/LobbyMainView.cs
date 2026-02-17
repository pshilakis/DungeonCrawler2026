using UnityEngine;
using PGS.UI;
using PGS.Utilities;

namespace PGS
{
    public class LobbyMainView : GameView<LobbyState>
    {
		[SerializeField] private GameSelectButtonOption newGameButton;
        //[SerializeField] private ButtonHandler newGameButton;
        [SerializeField] private ButtonHandler continueGameButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			newGameButton.button.OnPress += LoadNewGame;
			continueGameButton.OnPress += LoadExistingGame;
		}

		protected override void OnDisable()
		{
			newGameButton.button.OnPress -= LoadNewGame;
			continueGameButton.OnPress -= LoadExistingGame;
			base.OnDisable();
		}

		private void LoadNewGame()
		{
			LobbyState.OnNewGameButtonPressed?.Invoke(newGameButton.MapData);
		}

		private void LoadExistingGame()
		{
			LobbyState.OnContinueButtonPressed?.Invoke();
		}
	}
}
