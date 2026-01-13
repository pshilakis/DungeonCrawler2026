using PGS.Utilities;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
	[DisallowMultipleComponent]
    public class MapManager : MonoBehaviour
    {
		[SerializeField] private MapTile[] startTiles;
		public MapTile[] StartTiles { get { return startTiles; } }

		private MapData m_MapData;
		private Dictionary<Character, MapTile> m_OccupiedTiles = new Dictionary<Character, MapTile>();

		private void OnEnable()
		{
			MapTile.OnTileUpdated += UpdateTile;
		}

		private void OnDisable()
		{
			MapTile.OnTileUpdated -= UpdateTile;
		}

		/// <summary>
		/// Links this instance of a map to its corresponding MapData object
		/// </summary>
		/// <param name="data"></param>
		public void SetData(MapData data)
		{
			m_MapData = data;
		}

		private void UpdateTile(MapTile tile, Character character)
		{
			Debug.Log($"UH OH @ {tile.gameObject.name}");

			if (character != null)
			{
				m_OccupiedTiles[character] = tile;
				//pause the character moving
				//trigger any tile effects
				//continue character moves (if any remain)
			}
		}

		private void OnDrawGizmos()
		{
			foreach (MapTile tile in startTiles)
			{
				Gizmos.color = new Color(0, 0.3f, 1f, 0.5f);
				Gizmos.DrawSphere(tile.Center, 0.5f);
			}
		}
	}
}
