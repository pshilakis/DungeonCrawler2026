using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class SceneStateManager<TGameState, TSceneState> : MonoBehaviour
		where TGameState : GameState
		where TSceneState : IState
	{
        protected StateMachine<TSceneState> m_SceneStateMachine = new StateMachine<TSceneState>();

		/// <summary>
		/// Static event that announces its spawning in the scene so that GameStates 
		/// </summary>
		public static Action<SceneStateManager<TGameState, TSceneState>> OnSceneManagerOwnerRequest;

		protected virtual void Awake()
		{
			OnSceneManagerOwnerRequest?.Invoke(this);

		}
	}
}
