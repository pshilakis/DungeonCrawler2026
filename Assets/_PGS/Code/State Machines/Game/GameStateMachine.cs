using UnityEngine;

namespace PGS
{
    [System.Serializable]
    public class GameStateMachine : StateMachine<GameStateMachine>
    {
        [SerializeField] private LobbyState LobbyState;

        [ReadOnly][SerializeField] private GameState currentGameState;
        public GameState CurrentGameState {  get { return currentGameState; } }
    }
}
