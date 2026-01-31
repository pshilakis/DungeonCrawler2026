using System;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// A GameView ties a GameState to the in-scene elements (including menus, gameplay elements, etc.) and can send data to their specific state;
	/// </summary>
	/// <typeparam name="T"/>The type of GameState that this is tied to</typeparam>
	public abstract class GameView<T> : MonoBehaviour
		where T : IState
    {
        public T Owner { get; private set; }

        public static Func<GameView<T>,T> RequestOwner;

        protected virtual void Awake()
        {
			Owner = RequestOwner.Invoke(this);
        }

        protected virtual void OnEnable()
        {
			
		}

		protected virtual void OnDisable()
		{
			
		}

        protected virtual void OnDestroy()
        {

		}
	}
}
