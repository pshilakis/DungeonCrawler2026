using UnityEngine;

namespace PGS
{
    public class Turn
    {
        public Turn(int totalMoves)
        {
            m_TotalMoves = totalMoves; //set the total moves allowed
            RemainingMoves = totalMoves; //reset the moves remaining to the max value
        }

        private int m_TotalMoves; //The total number of moves allowed this turn
        
        private int m_RemainingMoves; //The remaining number of turns left for this turn
        public int RemainingMoves
        {
            get { return  m_RemainingMoves; }
            set { m_RemainingMoves = Mathf.Clamp(value, 0, m_TotalMoves); }
        }

        public void Move()
        {
            RemainingMoves--;
        }
    }
}
