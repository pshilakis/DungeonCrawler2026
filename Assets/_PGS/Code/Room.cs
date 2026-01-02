using System;
using UnityEngine;

namespace PGS
{
	[RequireComponent(typeof(RoomCollider))]
	[RequireComponent(typeof(BoxSelectionCollider))]
	[DisallowMultipleComponent]
    public class Room : MapEntity
    {
		[ReadOnly][SerializeField] private RoomCollider m_RoomCollider;
		private BoxSelectionCollider m_SelectionCollider;

		#region Unity Methods
		private void Reset()
		{
			m_RoomCollider = GetComponent<RoomCollider>(); //handle this in awake since we need to adjust this collider in editor
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			AdjustCollider(m_RoomCollider.Collider);
		}

		private void Awake()
		{
			m_SelectionCollider = GetComponent<BoxSelectionCollider>();
		}

		private void Start()
		{
			AdjustCollider(m_SelectionCollider.Collider);
		}

		private void OnEnable()
		{
			m_RoomCollider.OnCharacterEnter += OnCharacterEnterRoom;
			m_RoomCollider.OnCharacterExit += OnCharacterExitRoom;
		}

		private void OnDisable()
		{
			m_RoomCollider.OnCharacterEnter -= OnCharacterEnterRoom;
			m_RoomCollider.OnCharacterExit -= OnCharacterExitRoom;
		}
		#endregion

		private void AdjustCollider(BoxCollider collider)
		{
			float offsetY = -0.5f;
			collider.center = new Vector3((float)size.x / 2, offsetY, (float)size.y / 2);
			collider.size = new Vector3(size.x, 1f, size.y);
		}

		private void OnCharacterEnterRoom(Character character)
		{
			Debug.Log($"{character} entered room {gameObject.name} @ {DateTime.Now.TimeOfDay}");
		}

		private void OnCharacterExitRoom(Character character)
		{
			Debug.Log($"{character} exited room {gameObject.name} @ {DateTime.Now.TimeOfDay}");
		}
	}
}
