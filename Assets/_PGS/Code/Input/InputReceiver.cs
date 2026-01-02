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
		public void OnCursor(InputAction.CallbackContext context) { } //Doesn't do anything; just reports the cursor position

		public void OnClick(InputAction.CallbackContext context)
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
		public void OnNavigate(InputAction.CallbackContext context)
		{
		}

		public void OnSubmit(InputAction.CallbackContext context)
		{
		}

		public void OnCancel(InputAction.CallbackContext context)
		{
		}

		public void OnPoint(InputAction.CallbackContext context)
		{
		}

		public void OnRightClick(InputAction.CallbackContext context)
		{
		}

		public void OnMiddleClick(InputAction.CallbackContext context)
		{
		}

		public void OnScrollWheel(InputAction.CallbackContext context)
		{
		}

		public void OnTrackedDevicePosition(InputAction.CallbackContext context)
		{
		}

		public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
		{
		}
		#endregion
	}
}
