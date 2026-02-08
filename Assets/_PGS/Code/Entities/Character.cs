using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class Character : MapEntity
    {
		[SerializeField] private CharacterData data;

		public string CharacterName { get { return data.Name; }  }
		/// <summary>
		/// How fast the character moves from tile to tile
		/// </summary>
		[SerializeField] private float moveSpeed;

		/// <summary>
		/// The current tile that this character occupies
		/// </summary>
        [SerializeField] private MapTile m_CurrentTile;

		protected override void OnValidate()
		{
			base.OnValidate();
			moveSpeed = Mathf.Min(1f, moveSpeed); //Make sure this value can never go below 1
		}

        private void OnEnable()
        {
            MapTile.OnTileUpdated += UpdateCurrentTile; //change this to subscribe when it becomes this character's turn
        }

		private void OnDisable()
		{
			MapTile.OnTileUpdated -= UpdateCurrentTile;
		}

		public void Init(CharacterData data)
		{
			this.data = data;
			//set whatever info we need based on the data we've loaded
		}

		private void UpdateCurrentTile(MapTile tile, Character character)
		{
			if (character != this) { return; }
			m_CurrentTile = tile;
		}
	}
}
