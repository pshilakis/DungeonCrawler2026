using PGS.Utilities;
using UnityEngine;

namespace PGS
{
	public class BoxSelectionCollider : SelectionCollider<BoxCollider>
	{
		public override void GetCollider()
		{
			m_Collider = CommonUtilities.AddComponentToNewGameObject<BoxCollider>(this.transform, nameof(BoxSelectionCollider));
		}
	}
}
