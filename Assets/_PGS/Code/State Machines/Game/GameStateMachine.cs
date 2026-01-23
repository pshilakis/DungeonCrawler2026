using MEC;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
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
			LobbyState.OnNewGameButtonPressed += LoadPlayboard;

			lobbyState.gameObject.SetActive(false);
			playboardState.gameObject.SetActive(false);

			await SetState(bootState, false, false);
			await SetState(lobbyState, false, true);
		}

		public async void LoadPlayboard()
		{
			await SetState(playboardState, true, true);
		}

        private async UniTask SetState(GameState newState, bool animateIntro, bool animateOutro)
        {
            if (CurrentState == newState || newState == null) { return; }

            GameState previousState = CurrentState;
            Debug.Log($"<color=#00ccff>Game State Change:</color> {previousState?.GetType()} > {newState.GetType()}");

			if (previousState != null)
			{
				await SceneUtilities.ShowLoadScreen(animateIntro);
				await previousState.Exit();
			}

            CurrentState = newState;
			CurrentState.gameObject.SetActive(true);
			await CurrentState.Enter();

			if (previousState != null && CurrentState != bootState)
			{
				await SceneUtilities.HideLoadScreen(animateOutro);
			}

			//Add some kind of state.activate() to enable input etc after the loading screen has finished animating?
		}
	}
}
