using UnityEngine;

namespace PGS
{
    /// <summary>
    /// Configuration data used to customize and save character information
    /// </summary>
    [System.Serializable]
    public class CharacterData
    {
        [SerializeField] private string m_Name;
        public string Name { get { return m_Name; } }
        [SerializeField] private Vector3 m_Position;

        //Other configuration stuff
        //[SerializeField] private Color shirtColor;

        public CharacterData(string name)
        {
            m_Name = name;
        }

        public CharacterData(string name, Vector3 position)
        {
            m_Name = name;
            m_Position = position;
        }
    }
}
