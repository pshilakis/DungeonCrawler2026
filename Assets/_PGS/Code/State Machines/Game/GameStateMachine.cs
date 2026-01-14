using MEC;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameStateMachine, GameState>
    {
        [SerializeField] private BootState bootState;
        [SerializeField] private LobbyState lobbyState;
        [SerializeField] private BoardState boardState;

        private bool m_InitComplete = false;
        private CoroutineHandle m_SetStateCoroutine;

        public async void Initialize()
        {
			m_SetStateCoroutine = Timing.RunCoroutineSingleton(SetState(bootState, false, false), m_SetStateCoroutine, SingletonBehavior.Wait);
			m_SetStateCoroutine = Timing.RunCoroutineSingleton(SetState(lobbyState, false, true), m_SetStateCoroutine, SingletonBehavior.Wait);
		}

        public IEnumerator<float> SetState(GameState newState, bool animateIntro, bool animateOutro)
        {
            if (CurrentState == newState) { yield break; }

            GameState previousState = CurrentState;
            CurrentState = newState;
            Debug.Log($"<color=#00ccff>Game State Change:</color> {previousState?.GetType()} > {CurrentState.GetType()}");

            if (CurrentState.RequireLoadScreenOnEnter && m_InitComplete)
            {
                yield return Timing.WaitUntilDone(SceneUtilities.ShowLoadScreen(animateIntro));
			}

			if (previousState != null) //unload the previous state if there was one
			{
				previousState.Exit();
            }

			CurrentState.Enter();

            if (m_InitComplete)
            {
				yield return Timing.WaitUntilDone(SceneUtilities.HideLoadScreen(animateOutro)); //Hide Load Screen
			}
            else
            {
				m_InitComplete = true;
			}
		}
	}
}
