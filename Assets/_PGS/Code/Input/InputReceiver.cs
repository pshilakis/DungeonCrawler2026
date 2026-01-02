using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PGS
{
    //[CreateAssetMenu(fileName = "InputReceiver", menuName = "Scriptable Objects/InputReceiver")]
    public class InputReceiver : ScriptableObject, PlayerControls.IPlayerActions
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
        }

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
	}
}
