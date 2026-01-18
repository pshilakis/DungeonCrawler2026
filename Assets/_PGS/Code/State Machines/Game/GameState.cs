using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameState : State<GameState>
    {
        public abstract bool RequireLoadScreenOnEnter { get; }

        [SerializeField] protected SceneData[] requiredScenes;
    }
}
