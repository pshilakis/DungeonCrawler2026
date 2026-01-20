using PGS.Utilities;
using UnityEngine;

namespace PGS.UI
{
	[DisallowMultipleComponent]
	public class DisableRaycastTargetGraphics : MonoBehaviour
	{
		private enum ObjectsToDisable
		{
			SELF,
			CHILDREN,
			SELF_AND_CHILDREN
		}

		[SerializeField] private ObjectsToDisable targetsToDisable;

		private void Awake()
		{
			switch (targetsToDisable)
			{
				case ObjectsToDisable.SELF:
					RaycastUtilities.DisableRaycastTarget(transform);
					break;
				case ObjectsToDisable.CHILDREN:
					RaycastUtilities.DisableChildRaycastTargets(transform);
					break;
				case ObjectsToDisable.SELF_AND_CHILDREN:
					RaycastUtilities.DisableRaycastTarget(transform);
					RaycastUtilities.DisableChildRaycastTargets(transform);
					break;
			}
		}
	}
}

