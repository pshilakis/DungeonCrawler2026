using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace PGS
{
    /// <summary>
    /// A GameView ties a GameState to the in-scene elements (including menus, gameplay elements, etc.)
    /// </summary>
    public abstract class GameView : MonoBehaviour
    {
        private GameViewManager m_Manager;
        public Action<GameView> OnViewInitialized;
        public Action<GameView> OnViewActivate;
        public Action<GameView> OnViewDeactivate;

        protected CancellationTokenSource m_CancellationTokenSource = new CancellationTokenSource();
        protected CancellationToken ct;

        protected virtual void Awake()
        {
			ct = m_CancellationTokenSource.Token;
        }

        protected virtual void OnEnable()
        {
			
		}

		protected virtual void OnDisable()
		{
			
		}

        protected virtual void OnDestroy()
        {
			m_CancellationTokenSource.Cancel();
		}

        public void RegisterManager(GameViewManager manager)
        {
			m_Manager = manager;
        }

	}
}
