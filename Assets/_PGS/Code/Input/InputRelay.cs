using UnityEngine;

namespace PGS
{
    public struct PointerInfo
    {
        public Vector2 screenPosition;

        /// <summary>
        /// An non-normalized direction delta
        /// </summary>
        public Vector2 delta;
        public Vector3 worldPosition;

        public PointerInfo(Vector2 screenPosition, Vector2 delta, Vector3 worldPosition)
        {
            this.screenPosition = screenPosition;
            this.delta = delta;
            this.worldPosition = worldPosition;
        }

        public override string ToString()
        {
            return $"ScreenPosition: {screenPosition} | WorldPosition: {worldPosition}\nDelta: {delta}";
        }
    }

    /// <summary>
    /// Translates and relays input into formats that we can use elsewhere in the game
    /// </summary>
    public class InputRelay : MonoBehaviour
    {
        public InputReceiver Input { get; private set; }

        private Vector2 PointerPosition { get { return Input.Controls.Player.PointerPosition.ReadValue<Vector2>(); } }
        private Vector2 PointerDelta { get { return Input.Controls.Player.PointerDelta.ReadValue<Vector2>(); } }

        public PointerInfo Pointer
        {
            get
            {
                return new PointerInfo(
                    screenPosition: PointerPosition,
                    delta: PointerDelta,
                    worldPosition: GetCursorWorldPosition()
                    );
            }
        }

        private Vector3 WorldGridPosition { get { return GetWorldGridPointUnderCursor(); } }

		public const double HOLD_THRESHOLD = 0.25f;
		public const float DRAG_THRESHOLD = 5f;

		public void InitializeInput()
        {
            Input = InputReceiver.CreateNewInputReceiverInstance();
            Input.Initialize();

			//Move these eventually to when we actually need to start the controls in certain game states
			//Input.Controls.Player.Enable();
        }

		//private void PointerClick(TimeSpan time)
		//{
  //          if (EventSystem.current == null || EventSystem.current.IsPointerOverGameObject()) { return; }
  //          Debug.Log($">> PRESS @ {time}");
		//}

  //      private void PointerRelease(TimeSpan time)
  //      {
  //          Debug.Log($">> RELEASE @ {time}");
  //      }

  //      private void PointerHold(TimeSpan time)
  //      {
		//	Debug.Log($">> HOLD @ {time}");
		//}

        private Vector3Int GetWorldGridPointUnderCursor()
        {
            Vector3 cast = GetCursorWorldPosition();
			Vector3Int tile = new Vector3Int((int)cast.x, (int)cast.y, (int)cast.z);
			//Debug.Log($"Pointer: ({PointerPosition.x},{PointerPosition.y}) | Grid : {tile}");
			return tile;
		}

        public Vector3 GetCursorWorldPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(PointerPosition);
			Plane p = new Plane(Vector3.up, 0);

            if (p.Raycast(ray, out float distance))
            {
//#if UNITY_EDITOR
//				Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
//#endif
                Vector3 result = ray.GetPoint(distance);
                return ray.GetPoint(distance);
            }
            else
            {
                return Vector3.zero;
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying) { return; }

            Vector3 offset = new Vector3(0.5f, -0.5f, 0.5f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(WorldGridPosition + offset, Vector3.one);        
		}
	}
}
