using Cysharp.Threading.Tasks;
using System;

namespace PGS
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}
