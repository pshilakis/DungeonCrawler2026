using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MEC;
using PGS.Utilities;
using UnityEngine.EventSystems;

namespace PGS
{
	/// <summary>
	/// Reads the raw inputs from the game and fires any custom events we might need
	/// </summary>
    //[CreateAssetMenu(fileName = "InputReceiver", menuName = "Scriptable Objects/InputReceiver")]
    public class InputReceiver : ScriptableObject, PlayerControls.IPlayerActions, PlayerControls.IUIActions
    {
        public PlayerControls Controls { get; private set; }

		private bool m_HasDragPerformed = false;
		private CoroutineHandle m_DragCoroutine;

		#region Events
		public Action<TimeSpan> OnPointerDown;
        public Action<TimeSpan> OnPointerUp;
		public Action<TimeSpan> OnPointerHold;
		#endregion

        public static InputReceiver CreateNewInputReceiverInstance()
        {
            return ScriptableObject.CreateInstance<InputReceiver>();
        }

        public void Initialize()
        {
            Controls = new PlayerControls();
            Controls.Player.SetCallbacks(this);
			Controls.UI.SetCallbacks(this);
        }

		#region Player Actions
		void PlayerControls.IPlayerActions.OnPointerPosition(InputAction.CallbackContext context) { } //Doesn't do anything; just reports the cursor position

		void PlayerControls.IPlayerActions.OnPointerDelta(InputAction.CallbackContext context) { } //Doesn't do anything; just reports the cursor delta

		void PlayerControls.IPlayerActions.OnClick(InputAction.CallbackContext context)
		{
			if (EventSystem.current == null) { return; }
			if (EventSystem.current.IsPointerOverGameObject()) { return; }

			switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnPointerDown?.Invoke(DateTime.Now.TimeOfDay);
					m_DragCoroutine = Timing.RunCoroutineSingleton(HoldCoroutine(Controls.Player.PointerPosition.ReadValue<Vector2>()), m_DragCoroutine, SingletonBehavior.Overwrite);
					break;
				case InputActionPhase.Performed:
					if (!m_HasDragPerformed)
					{
						OnPointerHold?.Invoke(DateTime.Now.TimeOfDay);
					}
					break;
                case InputActionPhase.Canceled:
					StopDrag();
                    OnPointerUp?.Invoke(DateTime.Now.TimeOfDay);

                    break;
            }
		}

		void PlayerControls.IPlayerActions.OnMoveCamera(InputAction.CallbackContext context)
		{
			//Debug.Log(Controls.Player.MoveCamera.ReadValue<Vector2>());
		}

		private IEnumerator<float> HoldCoroutine(Vector2 pressPosition)
		{
			double holdTime = 0f;
			//while (holdTime < holdThreshold || (Controls.Player.Cursor.ReadValue<Vector2>() - pressPosition).SqrMagnitude() <= dragDelta * dragDelta)
			while (!m_HasDragPerformed && VectorUtilities.IsWithinRangeSqrMagnitude(Controls.Player.PointerPosition.ReadValue<Vector2>(), pressPosition, InputRelay.DRAG_THRESHOLD))
			{
				//Debug.Log($"{holdTime < InputRelay.HOLD_THRESHOLD} : {holdTime}/{InputRelay.HOLD_THRESHOLD}");
				yield return Timing.WaitForOneFrame;
				holdTime += Timing.DeltaTime;
			}

			OnPointerHold?.Invoke(DateTime.Now.TimeOfDay);
			m_HasDragPerformed = true;
		}

		private void StopDrag()
		{
			m_HasDragPerformed = false;
			Timing.KillCoroutines(m_DragCoroutine);
		}
		#endregion

		#region UI Actions
		void PlayerControls.IUIActions.OnNavigate(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnSubmit(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnCancel(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnPoint(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnClick(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnRightClick(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnMiddleClick(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnScrollWheel(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnTrackedDevicePosition(InputAction.CallbackContext context)
		{
		}

		void PlayerControls.IUIActions.OnTrackedDeviceOrientation(InputAction.CallbackContext context)
		{
		}
		#endregion
	}
}
