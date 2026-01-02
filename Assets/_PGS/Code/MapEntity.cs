using System.ComponentModel;
using UnityEngine;

namespace PGS
{
	[DisallowMultipleComponent]
	public class MapEntity : MonoBehaviour
	{
		[SerializeField] protected Vector2Int size = Vector2Int.one;

		protected virtual void OnValidate()
		{
			//Clamp the size values to minimum of 1 so we can't set invalid values in Editor
			size.x = Mathf.Max(size.x, 1);
			size.y = Mathf.Max(size.y, 1);
		}
	}
}
