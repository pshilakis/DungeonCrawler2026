using System.ComponentModel;
using UnityEngine;

namespace PGS
{
	[DisallowMultipleComponent]
	[SelectionBase]
	public class MapEntity : MonoBehaviour
	{
		[SerializeField] protected Vector2Int size = Vector2Int.one;

		public Vector3 Center
		{
			get
			{
				return new Vector3(
					transform.position.x + (size.x / 2f),
					transform.position.y,
					transform.position.z + (size.y / 2f)
					);
			}
		}

		protected virtual void OnValidate()
		{
			ClampSize();
		}

		/// <summary>
		/// Clamp the size values to minimum of 1 so we can't set invalid values in Editor
		/// </summary>
		private void ClampSize()
		{
			size.x = Mathf.Max(size.x, 1);
			size.y = Mathf.Max(size.y, 1);
		}

		protected virtual void OnDrawGizmos()
		{
			//Gizmos.color = Color.black;
			//Gizmos.DrawSphere(Center, 0.1f);
		}
	}
}
