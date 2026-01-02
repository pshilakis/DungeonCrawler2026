using System;
using UnityEngine;

namespace PGS
{
	[ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class RoomCollider : MonoBehaviour
    {
		[ReadOnly][SerializeField] private BoxCollider m_Collider;

		public BoxCollider Collider
		{ 
			get
			{
				if (m_Collider == null)
				{
					m_Collider = GetComponent<BoxCollider>();
				}

				return m_Collider;
			}
		}

		public Action<Character> OnCharacterEnter;
		public Action<Character> OnCharacterExit;

		private void Awake()
		{
			Collider.isTrigger = true;
			//Set collider layer
		}

		private void OnTriggerEnter(Collider other)
		{
			if (TryGetComponent<Character>(out Character character))
			{
				OnCharacterEnter?.Invoke(character);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (TryGetComponent<Character>(out Character character))
			{
				OnCharacterExit?.Invoke(character);
			}
		}
	}
}
