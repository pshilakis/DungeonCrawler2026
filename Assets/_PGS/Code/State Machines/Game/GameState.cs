using Animancer;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameState : MonoBehaviour, IState
    {
        public abstract bool RequireLoadScreenOnEnter { get; }

        [SerializeField] protected SceneData[] requiredScenes;

        [SerializeField] protected ClipTransition customIntro;
        [SerializeField] protected ClipTransition customOutro;

		public abstract UniTask Enter();

        public abstract UniTask Exit();
	}
}
