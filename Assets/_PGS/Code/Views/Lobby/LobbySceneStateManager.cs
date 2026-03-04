using UnityEngine;

namespace PGS
{
    public class LobbySceneStateManager : SceneStateManager<LobbyState, LobbySceneState>
    {
        [SerializeField] private LobbySceneState defaultState;
		//[SerializeField] private LobbySceneState 

		protected override async void Awake()
		{
			await m_SceneStateMachine.SetState(defaultState);
			base.Awake();
		}
	}
}
