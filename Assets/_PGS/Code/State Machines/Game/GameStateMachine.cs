using UnityEngine;
using Cysharp.Threading.Tasks;

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

        private async UniTask SetState(GameState newState, bool animateIntro, bool animateOutro)
        {
            if (CurrentState == newState || newState == null) { return; }

            GameState previousState = CurrentState;
            Debug.Log($"<color=#00ccff>{typeof(GameState)} Change:</color> {previousState?.GetType()} > {newState.GetType()}");

			if (previousState != null)
			{
				if (previousState is IControlInput)
				{
					IControlInput input = previousState as IControlInput;
					input.DisableInputs();
				}

				if (newState is IRequireLoadScreen)
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

			//Enables Input after the loading screen has been hidden
			if (CurrentState is IControlInput)
			{
				IControlInput input = CurrentState as IControlInput;
				input.EnableInputs();
			}
		}
	}
}
