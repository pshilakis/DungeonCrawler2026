using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class Character : MapEntity
    {
        [SerializeField] private MapTile m_CurrentTile;
        private Queue<MapTile> m_TileQueue; //The current path the character is going to take up to any branches
    }
}
