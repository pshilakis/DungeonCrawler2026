using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace PGS
{
    /// <summary>
    /// Stores the collection of available maps in the game in a dictionary to reference
    /// </summary>
    [CreateAssetMenu(fileName = nameof(MapDirectory), menuName = "PGS/Scriptable Objects/" + nameof(MapDirectory))]
    public class MapDirectory : ScriptableObject
    {
        [SerializeField] private MapData[] m_MapData;
        private Dictionary<string, MapData> m_Directory = new Dictionary<string, MapData>();
        public IReadOnlyDictionary<string, MapData> Directory { get { return m_Directory; } }

        public async UniTask BuildDirectory()
        {
			foreach (MapData map in m_MapData)
			{
				m_Directory.Add(map.ID, map);
			}

			Debug.Log($"{nameof(MapDirectory)} has been built @ {DateTime.Now.TimeOfDay}", this);
		}
    }
}
