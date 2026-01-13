using UnityEngine;

namespace PGS
{
    public abstract class GameState : State<GameState>
    {
        [SerializeField] private SceneData[] requiredScenes;
    }
}
