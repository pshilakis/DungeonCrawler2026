using UnityEngine;
using PGS.UI;
using PGS.Utilities;

namespace PGS
{
    public class LobbySceneState : SceneState<LobbyState>
    {
		[SerializeField] private GameSelectButtonOption newGameButton;
        //[SerializeField] private ButtonHandler newGameButton;
        [SerializeField] private ButtonHandler continueGameButton;

		[SerializeField] private ButtonTriggerDefinition testMenu;

		protected void OnEnable()
		{
			newGameButton.button.OnPress += LoadNewGame;
			//continueGameButton.OnPress += LoadExistingGame;
		}

		protected void OnDisable()
		{
			newGameButton.button.OnPress -= LoadNewGame;
			//continueGameButton.OnPress -= LoadExistingGame;
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
