using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    public class LobbyViewManager : GameViewManager
    {
        [SerializeField] private LobbyMainView mainView;

		protected async override void Awake()
		{
			mainView.RegisterManager(this);
			base.Awake();
		}
	}
}
