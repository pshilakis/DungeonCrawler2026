using UnityEngine;

namespace PGS
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string m_Name;
        [SerializeField] private string m_Description;
        [SerializeField] private GameObject m_Prefab;
    }
}
