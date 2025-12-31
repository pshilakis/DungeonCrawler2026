using UnityEngine;
using UnityEngine.InputSystem;

namespace PGS
{
    //[CreateAssetMenu(fileName = "InputReceiver", menuName = "Scriptable Objects/InputReceiver")]
    public class InputReceiver : ScriptableObject, PlayerControls.IPlayerActions
    {
        public PlayerControls Controls { get; private set; }
		#region Events
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
	}
}
