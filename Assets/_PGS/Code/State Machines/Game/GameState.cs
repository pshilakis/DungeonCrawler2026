using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameState : MonoBehaviour, IState
    {
        public abstract bool RequireLoadScreenOnEnter { get; }

        [SerializeField] protected SceneData[] requiredScenes;

		public abstract UniTask Enter();

        public abstract UniTask Exit();
	}
}
