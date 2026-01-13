using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameState : State<GameState>
    {
        public abstract bool RequireLoadScreenOnEnter { get; }

        [SerializeField] protected SceneData[] requiredScenes;

        /// <summary>
        /// Event when a state is first entered
        /// </summary>
        public Action OnStateEnter;

        /// <summary>
        /// Event when a state has been entered and finishes initializing
        /// </summary>
        public Action OnStateInitialized;
        
        /// <summary>
        /// Event when a state is told to Exit
        /// </summary>
        public Action OnStateExitState;

        /// <summary>
        /// Event when a state has completed Exiting
        /// </summary>
        public Action OnStateExitComplete;
    }
}
