using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        [SerializeField] private BootState bootState;
        [SerializeField] private LobbyState lobbyState;
        [SerializeField] private BoardState boardState;

        [ReadOnly][SerializeField] private GameState m_CurrentState;
        public GameState CurrentGameState
        { 
            get { return m_CurrentState; }
            private set { m_CurrentState = value; }
        }

        public async void Initialize()
        {
           await SetState(bootState);
           await SetState(lobbyState);
        }

        public async Task SetState(GameState newState)
        {
            if (CurrentGameState == newState) { return; }

            GameState previousState = CurrentGameState;
            CurrentGameState = newState;

            if (CurrentGameState.RequireLoadScreenOnEnter)
            {
                //Show load screen
            }

            if (previousState != null) //unload the previous state if there was one
			{
				await previousState.Exit();
            }

			await CurrentGameState.Enter();

            //Hide Load Screen
		}
    }
}
