using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private CameraController m_Camera;
		public CameraController MapCameraController {  get { return m_Camera; } }

        [SerializeField] private Map m_LoadedMap;

		private Dictionary<Character, MapTile> m_OccupiedTiles = new Dictionary<Character, MapTile>();

		private PlayboardState m_State;

		#region Events
		public static Func<MapManager, PlayboardState> OnManagerLoaded;
		#endregion

		private void Awake()
		{
			m_State = OnManagerLoaded?.Invoke(this);
		}

		//private void OnEnable()
		//{
		//	MapTile.OnTileUpdated += UpdateTile;
		//}

		//private void OnDisable()
		//{
		//	MapTile.OnTileUpdated -= UpdateTile;
		//}

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

		public async UniTask LoadMap(MapData data)
        {
            m_LoadedMap = data.InstantiateMap();
			m_Camera.SetBounds(m_LoadedMap.MapBounds);
        }
    }
}
