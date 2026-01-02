using PGS.Utilities;
using UnityEngine;

namespace PGS
{
    public abstract class SelectionCollider<T> : MonoBehaviour where T : Collider
    {
		[Tooltip("This collider is added in Awake() at runtime")]
		[ReadOnly][SerializeReference] protected T m_Collider;

		public T Collider { get { return m_Collider; } }

		protected virtual void Awake()
		{
			GetCollider();
			//Set collider to correct selection layer
		}

		public abstract void GetCollider();
	}
}
