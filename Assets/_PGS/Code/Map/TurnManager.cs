using UnityEngine;

namespace PGS
{
    public class TurnManager
    {
		/// <summary>
		/// The current character whose turn it is
		/// </summary>
		private Character m_CurrentCharacter;

		/// <summary>
		/// The character's current turn data
		/// </summary>
		private Turn m_CurrentTurn;

		public void StartNewTurn(Character character, Turn turn)
		{
			m_CurrentCharacter = character;
			m_CurrentTurn = turn;
		}
	}
}
