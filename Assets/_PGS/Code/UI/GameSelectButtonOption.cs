using PGS.UI;
using UnityEngine;

namespace PGS
{
    public class GameSelectButtonOption : MonoBehaviour
    {
        [SerializeField] private MapData m_MapData;
        public MapData MapData {  get { return m_MapData; } }
        public ButtonHandler button;

        public void Awake()
        {
            AddOption(m_MapData); //Eventually move this out of Awake and populate a new list with these options dynamically after clicking the "New Game" button
        }

        public void AddOption(MapData data)
        {
			m_MapData = data;
            button.SetButtonText(data.MapName);
		}

    }
}
