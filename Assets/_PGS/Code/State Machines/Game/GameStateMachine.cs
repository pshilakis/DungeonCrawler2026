using MEC;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameStateMachine, GameState>
    {
        [SerializeField] private BootState bootState;
        [SerializeField] private LobbyState lobbyState;
        [SerializeField] private PlayboardState playboardState;

        public async UniTask Initialize()
        {
			await SetState(bootState, false, false);
			await SetState(lobbyState, false, true);
		}

        public async UniTask SetState(GameState newState, bool animateIntro, bool animateOutro)
        {
            if (CurrentState == newState) { return; }

            GameState previousState = CurrentState;
            Debug.Log($"<color=#00ccff>Game State Change:</color> {previousState?.GetType()} > {newState.GetType()}");

			if (previousState != null)
			{
				await SceneUtilities.ShowLoadScreen(animateIntro);
				await previousState.Exit();
			}

            CurrentState = newState;
			await CurrentState.Enter();

			if (previousState != null && CurrentState != bootState)
			{
				await SceneUtilities.HideLoadScreen(animateOutro);
			}
		}
	}
}
