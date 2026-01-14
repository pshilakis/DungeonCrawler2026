using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PGS
{
    //[CreateAssetMenu(fileName = "InputReceiver", menuName = "Scriptable Objects/InputReceiver")]
    public class InputReceiver : ScriptableObject, PlayerControls.IPlayerActions, PlayerControls.IUIActions
    {
        public PlayerControls Controls { get; private set; }

        #region Events
        public Action<TimeSpan> OnPointerDown;
        public Action<TimeSpan> OnPointerUp;
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
		void PlayerControls.IPlayerActions.OnCursor(InputAction.CallbackContext context) { } //Doesn't do anything; just reports the cursor position

		void PlayerControls.IPlayerActions.OnClick(InputAction.CallbackContext context)
		{
            switch (context.phase)
            {
                case InputActionPhase.Started:
                    OnPointerDown?.Invoke(DateTime.Now.TimeOfDay);
                    break;
                case InputActionPhase.Canceled:
                    OnPointerUp?.Invoke(DateTime.Now.TimeOfDay);
                    break;
            }
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
