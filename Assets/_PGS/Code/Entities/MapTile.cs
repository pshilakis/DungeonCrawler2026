using System;
using UnityEngine;

namespace PGS
{
	[RequireComponent(typeof(TileCollider))]
	[RequireComponent(typeof(BoxSelectionCollider))]
	[DisallowMultipleComponent]
    public class MapTile : MapEntity
    {
		[ReadOnly][SerializeField] private TileCollider m_TileCollider;
		private BoxSelectionCollider m_SelectionCollider;

		[SerializeField] private MapTile[] connections;

		#region Unity Methods
		private void Reset()
		{
			m_TileCollider = GetComponent<TileCollider>(); //handle this in awake since we need to adjust this collider in editor
		}

		protected override void OnValidate()
		{
			base.OnValidate();
			AdjustCollider(m_TileCollider.Collider);
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
			m_TileCollider.OnCharacterEnter += OnCharacterEnterTile;
			m_TileCollider.OnCharacterExit += OnCharacterExitTile;
		}

		private void OnDisable()
		{
			m_TileCollider.OnCharacterEnter -= OnCharacterEnterTile;
			m_TileCollider.OnCharacterExit -= OnCharacterExitTile;
		}

		protected override void OnDrawGizmos()
		{
			base.OnDrawGizmos();

			if (connections.Length == 0) { return; }
			foreach (MapTile tile in connections)
			{
				Gizmos.color = Color.yellowGreen;
				Gizmos.DrawLine(Center, tile.Center);
			}

		}
		#endregion

		private void AdjustCollider(BoxCollider collider)
		{
			float offsetY = 0.5f;
			collider.center = new Vector3((float)size.x / 2, offsetY, (float)size.y / 2);
			collider.size = new Vector3(size.x, 1f, size.y);
		}

		private void OnCharacterEnterTile(Character character)
		{
			Debug.Log($"{character} entered room {gameObject.name} @ {DateTime.Now.TimeOfDay}");
		}

		private void OnCharacterExitTile(Character character)
		{
			Debug.Log($"{character} exited room {gameObject.name} @ {DateTime.Now.TimeOfDay}");
		}


	}
}
