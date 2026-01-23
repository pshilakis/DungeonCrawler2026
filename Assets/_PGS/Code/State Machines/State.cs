using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace PGS
{
    public abstract class State<T>
    {
		protected CancellationTokenSource cts = new CancellationTokenSource();

		public abstract UniTask Enter();
        public abstract UniTask Exit();
    }
}
