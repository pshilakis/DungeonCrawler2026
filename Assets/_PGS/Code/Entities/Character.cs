using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class Character : MapEntity
    {
		/// <summary>
		/// How fast the character moves from tile to tile
		/// </summary>
		[SerializeField] private float moveSpeed;
		private string m_CharacterName;

		/// <summary>
		/// The current tile that this character occupies
		/// </summary>
        [SerializeField] private MapTile m_CurrentTile;

		public Action OnTurnStart;
		public Action OnTurnEnd;

		protected override void OnValidate()
		{
			base.OnValidate();
			moveSpeed = Mathf.Min(1f, moveSpeed); //Make sure this value can never go below 1
		}

        private void OnEnable()
        {
            MapTile.OnTileUpdated += UpdateCurrentTile;
        }

		private void OnDisable()
		{
			MapTile.OnTileUpdated -= UpdateCurrentTile;
		}

		public void Init(string playerName)
		{
			m_CharacterName = playerName;
		}

		private void UpdateCurrentTile(MapTile tile, Character character)
		{
			if (character != this) { return; }
			m_CurrentTile = tile;
		}
	}
}
