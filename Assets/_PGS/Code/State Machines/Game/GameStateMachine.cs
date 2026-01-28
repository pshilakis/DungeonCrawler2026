using UnityEngine;
using Cysharp.Threading.Tasks;
using PGS.Utilities;
using System;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameState>
    {
        [SerializeField] private BootState bootState;
        [SerializeField] private LobbyState lobbyState;
        [SerializeField] private PlayboardState playboardState;

        public async UniTask Initialize()
        {
			//Subscribe to state events
			LobbyState.OnNewGameButtonPressed += StartNewGame;
			//LobbyState.OnContinueButtonPressed += ContinueGame;

			lobbyState.gameObject.SetActive(false);
			playboardState.gameObject.SetActive(false);

			await SetState(bootState, false, false);
			await SetState(lobbyState, false, true);
		}

		public async void StartNewGame(MapData data)
		{
			Debug.Log($"StartNewGame() > {data.MapName}");
			playboardState.SetMapData(data);
			await SetState(playboardState, true, true);
		}

		private async void ContinueGame(MapData data)
		{
			playboardState.SetMapData(data);
			await SetState(playboardState, true, true);
		}

		private async UniTask SetState(GameState newState, bool animateIntro, bool animateOutro)
        {
            if (CurrentState == newState || newState == null) { return; }

            GameState previousState = CurrentState;
			string previousStateName = previousState != null ? previousState.GetType().ToString() : "<color=#ff0000>null</color>";
			Debug.Log($"<color=#00ccff>{typeof(GameState)} Change:</color> {previousStateName} > {newState.GetType()}");

			IControlInput input;

			if (previousState != null)
			{
				if (CommonUtilities.IsConvertable<GameState, IControlInput>(previousState, out input)) //Check if we need to disable any inputs
				{
					input.DisableInputs();
				}

				if (CommonUtilities.IsConvertable<GameState, IRequireLoadScreen>(newState)) //Check if we need to show a loading screen
				{
					await SceneUtilities.ShowLoadScreen(animateIntro);
				}

				await previousState.Exit();
			}

            CurrentState = newState;
			CurrentState.gameObject.SetActive(true);
			await CurrentState.Enter();

			if (previousState != null && CurrentState != bootState)
			{
				await SceneUtilities.HideLoadScreen(animateOutro);
			}

			if (CommonUtilities.IsConvertable<GameState, IControlInput>(CurrentState, out input))//Enables Input after the loading screen has been hidden
			{
				input.EnableInputs();
			}
		}
	}
}
