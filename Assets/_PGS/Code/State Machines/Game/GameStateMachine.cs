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

        public async Task Initialize()
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
			
   //         if (CurrentState.RequireLoadScreenOnEnter && CurrentState != bootState) //If we require a load screen and we're not entering boot
			//{
			//	yield return Timing.WaitUntilDone(SceneUtilities.ShowLoadScreen(animateIntro));
			//}

			//if (previousState != null) //unload the previous state if there was one
			//{
			//	yield return Timing.WaitUntilTrue(() => previousState.Exit()); //Can I use a Func<bool> somehow to Timing.WaitUntilTrue() for this?
			//}

			//yield return Timing.WaitUntilTrue(() => CurrentState.Enter()); //Can I use a Func<bool> somehow to Timing.WaitUntilTrue() for this?

   //         if (SceneUtilities.LoadingScreenEnabled && CurrentState != bootState) //We don't need to animate the loading screen coming out of boot
   //         {
   //             Debug.Log($"START: {DateTime.Now.Second}");
   //             CoroutineHandle handle = SceneUtilities.HideLoadScreen(animateOutro);
			//	yield return Timing.WaitUntilDone(handle); //Hide Load Screen
   //             Debug.Log($"END: {DateTime.Now.Second}");
			//}
		}
	}
}
