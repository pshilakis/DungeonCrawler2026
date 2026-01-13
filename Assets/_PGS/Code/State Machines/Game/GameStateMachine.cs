using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameStateMachine, GameState>
    {
        [SerializeField] private BootState bootState;
        [SerializeField] private LobbyState lobbyState;
        [SerializeField] private BoardState boardState;

        public async void Initialize()
        {
           await SetState(bootState);
           await SetState(lobbyState);
        }

        public async Task SetState(GameState newState)
        {
            if (CurrentState == newState) { return; }

            GameState previousState = CurrentState;
            CurrentState = newState;
            Debug.Log($"<color=#00ccff>Game State Change:</color> Enter > {CurrentState.GetType()}");

            if (CurrentState.RequireLoadScreenOnEnter)
            {
                //Show load screen
            }

            if (previousState != null) //unload the previous state if there was one
			{
				await previousState.Exit();
            }

			await CurrentState.Enter();

            //Hide Load Screen
		}
    }
}
