using PGS.Utilities;
using UnityEngine;

namespace PGS
{
	public class CapsuleSelectionCollider : SelectionCollider<CapsuleCollider>
	{
		public override void GetCollider()
		{
			m_Collider = CommonUtilities.AddComponentToNewGameObject<CapsuleCollider>(this.transform, nameof(CapsuleSelectionCollider));
		}
	}
}
