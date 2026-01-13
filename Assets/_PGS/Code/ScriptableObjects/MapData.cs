using UnityEngine;

namespace PGS
{
    [CreateAssetMenu(fileName = "MapData", menuName = "PGS/Scriptable Objects/MapData")]
    public class MapData : ScriptableObject
    {
        [SerializeField] private MapManager mapPrefab;

        public MapManager InstantiateMap()
        {
            MapManager instance = GameObject.Instantiate(mapPrefab);
            instance.SetData(this);
            return instance;
        }
    }
}
