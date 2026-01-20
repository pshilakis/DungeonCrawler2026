using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PGS.Utilities
{
	/// <summary>
	/// Utilities for raycasts and raycast-specific functions
	/// </summary>
	public static class RaycastUtilities
	{
		public static bool GetClickedObject3D<T>(Vector2 clickPos, out T selected, LayerMask layerMask = default, Camera camera = null) where T : class
		{		
			Ray ray = camera.ScreenPointToRay(clickPos);
			selected = null;

			if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask))
			{
				if (hit.collider != null)
				{
					selected = hit.collider.GetComponentInParent<T>();
					if (selected != null)
					{
						return true;
					}
				}
			}

			return false;
		}

		public static bool GetClickedObject3D<T>(Vector3 from, Vector3 to, out T selected) where T : class
		{
			Ray ray = new Ray(from, to);
			RaycastHit hit;
			selected = null;

			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				if (hit.collider != null)
				{
					selected = hit.collider.GetComponentInParent<T>();

					if (selected != null)
					{
						return true;
					}
				}
			}

			return false;
		}

		/// <summary>
		/// Check the angle between some Vector and a target Vector and ensure that it's within a specific angle
		/// https://youtu.be/MB7d3MdVHwU?t=802
		/// </summary>
		public static bool IsWithinAngle(Vector3 self, Vector3 target, float angle, Vector3 selfForward, bool ignoreY = false)
		{
			float halfAngle = angle * 0.5f; //Calculate half the angle since we want it centered on the forward vector (meaning each side is really 1/2 the angle)
			float cone = Mathf.Cos(halfAngle * Mathf.Deg2Rad);
			Vector3 targetPosition = target; //the target position to check
			Vector3 direction = targetPosition - self; //Get the direction to the target position
			if (ignoreY) //if we don't care about the Y-position...
			{
				direction.y = 0; //..then set it to 0 before normalizing
			}
			Vector3.Normalize(direction);
			Debug.Log($"ARC HITBOX CHECK\nCone Angle: {cone}\nAngle from Origin to Target: {Vector3.Dot(selfForward, direction)}\n");
			return Vector3.Dot(selfForward, direction) > cone;

		}

		/// <summary>
		/// Check the angle between some Transform and a target Transform and ensure that it's within a specific angle
		/// </summary>
		public static bool IsWithinAngle(Transform self, Transform target, float angle, bool ignoreY = false)
		{
			return IsWithinAngle(self.position, target.position, angle, self.forward, ignoreY);
		}

		public static void DisableRaycastTarget(Transform transform)
		{
			LayerUtilities.SetLayer(transform, LayerUtilities.IGNORE_RAYCAST_LAYER_NAME);

			//Disable RaycastTarget on Graphic Components
			Graphic graphic = transform.GetComponent<Graphic>();
			if (graphic != null)
			{
				if (!graphic.raycastTarget) //skip over it if it already has raycast target disabled
				{
					return;
				}

				graphic.raycastTarget = false;
			}

			//Disable RaycastTarget on TMPro Components
			TextMeshProUGUI tmpro = transform.GetComponent<TextMeshProUGUI>();
			if (tmpro != null)
			{
				if (!tmpro.raycastTarget)
				{
					return;
				}

				tmpro.raycastTarget = false;
			}
		}

		public static void DisableChildRaycastTargets(Transform transform)
		{
			foreach (Transform child in transform)
			{
				DisableRaycastTarget(child);
				DisableChildRaycastTargets(child);
			}
		}


	}

}