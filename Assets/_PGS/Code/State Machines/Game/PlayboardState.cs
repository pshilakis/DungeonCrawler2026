using Animancer;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	[System.Serializable]
	public class PlayboardState : GameState, IControlInput, IRequireLoadScreen
	{
		[Header("Substates")]
		[SerializeField] private StartGameState startGameState;
		[SerializeField] private PlayGameState playGameState;
		private StateMachine<IState> m_States = new StateMachine<IState>();

		[Header("Game Info")]
		[SerializeField] private Character[] activeCharacters;
		[ReadOnly][SerializeField] private MapData m_CurrentMapData;
		[ReadOnly][SerializeField] private MapManager m_CurrentMapManager;
		public MapManager CurrentMap { get { return m_CurrentMapManager; } }

		#region IControlInput
		public void EnableInputs()
		{
			if (m_States.CurrentState == null) { return; }
			if (m_States.CurrentState is IControlInput)
			{
				IControlInput input = m_States.CurrentState as IControlInput;
				input.EnableInputs();
			}
		}

		public void DisableInputs()
		{
			if (m_States.CurrentState == null) { return; }
			if (m_States.CurrentState is IControlInput)
			{
				IControlInput input = m_States.CurrentState as IControlInput;
				input.DisableInputs();
			}
		}
		#endregion

		#region IRequireLoadScreen
		public ClipTransition CustomIntro { get { return m_CustomIntro; } }

		public ClipTransition CustomOutro { get { return m_CustomOutro; } }
		#endregion

		private void Awake()
		{
			startGameState.OnParentStateRequest += () => this;
			playGameState.OnParentStateRequest += () => this;

			MapManager.OnManagerLoaded += SetCurrentMapManager;

		}

		private void OnDestroy()
		{
			MapManager.OnManagerLoaded -= SetCurrentMapManager;
		}

		#region IState
		public override async UniTask Enter()
		{
			if (m_CurrentMapData == null)
			{
				Debug.LogError($"No {nameof(MapData)} has been set before entering {nameof(PlayboardState)}");
			}

			await SceneUtilities.LoadScenes(requiredScenes);
			m_CurrentMapManager.LoadMap(m_CurrentMapData);

			//Determine whether we're in a new game or a loaded existing game, and then set the correct substate
			await m_States.SetState(playGameState);
		}

		public override async UniTask Exit()
		{
			await m_States.CurrentState.Exit();
			gameObject.SetActive(false);
		}
		#endregion

		private PlayboardState SetCurrentMapManager(MapManager manager)
		{
			m_CurrentMapManager = manager;
			return this;
		}

		public void SetMapData(MapData data)
		{
			Debug.Log($"SETTING MAP DATA > {data.name}");
			m_CurrentMapData = data;
		}


	}
}
