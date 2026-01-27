using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
	[DisallowMultipleComponent]
    public class Map : MonoBehaviour
    {
		[SerializeField] private Bounds2D m_MapBounds;
		public Bounds2D MapBounds {  get { return m_MapBounds; } }

		[SerializeField] private MapTile[] m_StartTiles;
		public MapTile[] StartTiles { get { return m_StartTiles; } }

		private MapData m_MapData;

		/// <summary>
		/// Links this instance of a map to its corresponding MapData object
		/// </summary>
		/// <param name="data"></param>
		public void SetData(MapData data)
		{
			m_MapData = data;
		}

		#region Gizmos
		private void OnDrawGizmos()
		{
			DrawMapBoundsGizmo();
			DrawStartTileGizmos();
		}

		private void DrawMapBoundsGizmo()
		{
			//Draw the map boundary as a box
			Gizmos.color = Color.red;
			Vector3 center = new Vector3(GetFixedCoordinateWithOffset(m_MapBounds.Center.x), 0.5f, GetFixedCoordinateWithOffset(m_MapBounds.Center.z));
			Vector3 size = new Vector3(m_MapBounds.XMax, 1f, m_MapBounds.ZMax);
			Gizmos.DrawWireCube(center, size);
		}

		private void DrawStartTileGizmos()
		{
			foreach (MapTile tile in m_StartTiles)
			{
				Gizmos.color = new Color(0, 0.3f, 1f, 0.5f);
				Gizmos.DrawSphere(tile.Center, 0.5f);
			}
		}

		private float GetFixedCoordinateWithOffset(float coordinate)
		{
			const float offset = 0.5f;
			
			if (coordinate % 2f == 0)
			{
				return coordinate;
			}
			else
			{
				return coordinate + offset;
			}
		}
		#endregion
	}
}
