using Cysharp.Threading.Tasks;

namespace PGS
{
    public interface IState
    {
        public UniTask Enter();
        public UniTask Exit();
    }
}
