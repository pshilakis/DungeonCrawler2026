using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class CharacterManager : MonoBehaviour
    {
		[Header("Player Character References")]
		[SerializeField] private Character m_CharacterPrefab;

		/// <summary>
		/// The list of character data that lives outside of the scene
		/// </summary>
		[SerializeField] private List<CharacterData> m_CharacterDataList = new List<CharacterData>();
		public List<CharacterData> CharacterDataList {  get { return m_CharacterDataList; } }
		public int Count { get { return m_CharacterDataList.Count; } }

		private void Awake()
		{
			CharacterSelectView.OnNewCharacterCreated += AddCharacterData;
		}

		private void OnDestroy()
		{
			CharacterSelectView.OnNewCharacterCreated -= AddCharacterData;
		}

		public void AddCharacterData(CharacterData characterData)
		{
			m_CharacterDataList.Add(characterData);
		}

		public void SpawnCharactersFromData()
		{
			for (int i = 0; i < m_CharacterDataList.Count; i++)
			{
				Character character = GameObject.Instantiate(m_CharacterPrefab);
				character.Init(m_CharacterDataList[i]);
			}
		}
	}
}
