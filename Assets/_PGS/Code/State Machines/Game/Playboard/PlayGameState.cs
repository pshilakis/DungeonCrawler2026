using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// The Game state where we are actually playing the game (rolling, moving, getting points, etc.)
	/// </summary>
	public class PlayGameState : MonoBehaviour, IState, IControlInput
	{
		private PlayboardState m_Parent;
		//Substates
		//> Next Player Roll
		//> Player Move
		//> Player Reward

		[Header("Map Info")]
		[ReadOnly][SerializeField] private MapData m_CurrentMapData;
		[ReadOnly][SerializeField] private MapManager m_CurrentMapManager;
		public MapManager CurrentMap { get { return m_CurrentMapManager; } }

		private bool m_IsDraggingCamera;

		#region Events
		public Func<PlayboardState> OnParentStateRequest;
		#endregion

		#region IState
		public async UniTask Enter()
		{
			if (m_CurrentMapData == null)
			{
				Debug.LogError($"No {nameof(MapData)} has been set before entering {nameof(PlayboardState)}");
			}

			await m_CurrentMapManager.LoadMap(m_CurrentMapData); //Load map
		}

		public async UniTask Exit()
		{
			gameObject.SetActive(false);
		}
		#endregion

		#region IControlInput
		public void EnableInputs()
		{
			GameManager.Instance.InputRelay.Input.OnPointerHold += EnableCamera;
			GameManager.Instance.InputRelay.Input.OnPointerUp += DisableCamera;

			GameManager.Instance.InputRelay.Input.Controls.UI.Enable();
			GameManager.Instance.InputRelay.Input.Controls.Player.Enable();
		}

		public void DisableInputs()
		{
			GameManager.Instance.InputRelay.Input.Controls.UI.Disable();
			GameManager.Instance.InputRelay.Input.Controls.Player.Disable();
		}
		#endregion

		private void Awake()
		{
			m_Parent = OnParentStateRequest?.Invoke();
			MapManager.OnManagerLoaded += SetCurrentMapManager;
		}

		private void Update()
		{
			if (m_IsDraggingCamera)
			{
				CurrentMap.MapCameraController.MoveCameraInDirection(GameManager.Instance.InputRelay.Pointer.delta, Time.deltaTime, true);
			}
		}

		private void OnDestroy()
		{
			MapManager.OnManagerLoaded -= SetCurrentMapManager;
		}

		public void SetMapData(MapData data)
		{
			Debug.Log($"SETTING MAP DATA > {data.name}");
			m_CurrentMapData = data;
		}

		private PlayGameState SetCurrentMapManager(MapManager manager)
		{
			m_CurrentMapManager = manager;
			return this;
		}

		public void SetParentState(PlayboardState parent)
		{
			m_Parent = parent;
		}
		
		private void EnableCamera(TimeSpan span)
		{
			m_IsDraggingCamera = true;
		}

		private void DisableCamera(TimeSpan span)
		{
			m_IsDraggingCamera = false;
		}


	}
}
