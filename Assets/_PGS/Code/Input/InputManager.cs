using System;
using UnityEngine;

namespace PGS
{
    public class InputManager : MonoBehaviour
    {
        public InputReceiver Input { get; private set; }

        private Vector2 PointerPosition { get { return Input.Controls.Player.Cursor.ReadValue<Vector2>(); } }
        private Vector3 WorldGridPosition { get { return GetWorldGridPointUnderCursor(); } }

        public void InitializeInput()
        {
            Input = InputReceiver.CreateNewInputReceiverInstance();
            Input.Initialize();

            Input.OnPointerDown += PointerClick;
            Input.OnPointerUp += PointerRelease;

            Input.Controls.Player.Enable(); //Move this eventually to when we actually need to start the controls in certain game states
        }

		private void PointerClick(TimeSpan time)
		{
            Debug.Log($"press @ {time}");
		}

        private void PointerRelease(TimeSpan time)
        {
            Debug.Log($"release @ {time}");
        }

		private void Start()
        {
            InitializeInput();
        }

        private Vector3Int GetWorldGridPointUnderCursor()
        {
            Vector3 cast = GetCursorWorldPosition();
			Vector3Int tile = new Vector3Int((int)cast.x, (int)cast.y, (int)cast.z);
			//Debug.Log($"Pointer: ({PointerPosition.x},{PointerPosition.y}) | Grid : {tile}");
			return tile;
		}

        private Vector3 GetCursorWorldPosition()
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
