using UnityEngine;

namespace PGS
{
    [CreateAssetMenu(fileName = "MapData", menuName = "PGS/Scriptable Objects/MapData")]
    public class MapData : UniqueGUIDScriptableObjectBase
	{
        [ColoredHeader("Map Info", 14, true)]
        [SerializeField] private string m_MapName;
        [SerializeField] private Map m_MapPrefab;

        public Map InstantiateMap()
        {
            Map instance = GameObject.Instantiate(m_MapPrefab);
            instance.SetData(this);
            return instance;
        }
    }
}
