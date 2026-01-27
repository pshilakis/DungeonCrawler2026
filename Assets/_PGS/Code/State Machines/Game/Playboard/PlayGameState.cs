using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	public class PlayGameState : MonoBehaviour, IState, IControlInput
	{
		private PlayboardState m_Parent;
		//Substates
		//> Next Player Roll
		//> Player Move
		//> Player Reward

		private bool m_IsDraggingCamera;

		#region Events
		public Func<PlayboardState> OnParentStateRequest;
		#endregion

		private void Awake()
		{
			m_Parent = OnParentStateRequest?.Invoke();
		}

		public async UniTask Enter()
		{

		}

		public async UniTask Exit()
		{
			gameObject.SetActive(false);
		}

		private void Update()
		{
			if (m_IsDraggingCamera)
			{
				//m_CurrentMapManager.MapCameraController.MoveCameraInDirection(GameManager.Instance.InputRelay.Pointer.delta, Time.deltaTime, true);
			}
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
	}
}
