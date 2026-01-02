using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class Character : MapEntity
    {
        [SerializeField] private Room m_CurrentRoom;
        [SerializeField] private Queue<Room> m_RoomQueue;
    }
}
