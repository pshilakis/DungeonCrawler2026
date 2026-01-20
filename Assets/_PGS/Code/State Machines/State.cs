using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    public abstract class State<T>
    {
		/// <summary>
		/// Event when a state is first entered
		/// </summary>
		public Action OnEnter;

		/// <summary>
		/// Event when a state has been entered and finishes initializing
		/// </summary>
		public Action OnEnterComplete;

		/// <summary>
		/// Event when a state is told to Exit
		/// </summary>
		public Action OnExit;

		/// <summary>
		/// Event when a state has completed Exiting
		/// </summary>
		public Action OnExitComplete;

		protected CancellationTokenSource cts = new CancellationTokenSource();

		public abstract UniTask Enter();
        public abstract UniTask Exit();
    }
}
