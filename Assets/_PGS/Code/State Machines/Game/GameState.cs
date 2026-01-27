using Animancer;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameState : MonoBehaviour, IState
    {
        [SerializeField] protected SceneData[] requiredScenes;

        [SerializeField] protected ClipTransition m_CustomIntro;
        [SerializeField] protected ClipTransition m_CustomOutro;

		public abstract UniTask Enter();

        public abstract UniTask Exit();
	}
}
