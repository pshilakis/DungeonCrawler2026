using UnityEngine;

namespace PGS
{
    [ExecuteInEditMode]
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private bool m_Enable;

        [SerializeField] private Transform target;

        [Tooltip("X-axis rotation: Vertical rotation of the camera, up or down.")]
        [SerializeField] private Transform tilt;

        [Tooltip("Y-axis rotation: Horizontal rotation of the camera, left or right.")]
        [SerializeField] private Transform pan;

        [Tooltip("Z-axis rotation: Rotate camera around its axis")]
        [SerializeField] private Transform roll;

        [Tooltip("Z-axis: In or out movement")]
        [SerializeField] private Transform zoom;

        [Tooltip("X = Min, Y = Max, Z = Default Position")]
        [SerializeField] private Vector3 zoomRange;

        [SerializeField][Min(1f)] private float speed;

        private Bounds2D m_Bounds;

        public void SetBounds(Bounds2D bounds)
        {
            m_Bounds = bounds;
        }

        /// <summary>
        /// Moves the camera rig to a new position
        /// </summary>
        /// <param name="position"></param>
        public void SetPosition(Vector3 position)
        {
            target.transform.position = position;
        }

        public void MoveCameraInDirection(Vector2 directionXY, float deltaTime, bool invertDirection)
        {
            if (speed <= 0)
            {
                Debug.LogError($"You are trying to move camera ({this.gameObject.name}) but it's movespeed is 0!");
                return;
            }

            Vector3 directionXZ = new Vector3(directionXY.x, 0f, directionXY.y);
            if (invertDirection) //negate the directions since we want the camera to move in the opposite direction that we're dragging the camera in (but we want to be able to configure it since WASD should move in the same direction)
            {
                directionXZ = -directionXZ;
            }

			transform.position += directionXZ * speed * deltaTime;

            //Vector3 newPosition = transform.position + directionXZ * speed * deltaTime;
            //transform.position = bounds.ClampToBounds(newPosition);
        }
    }
}
