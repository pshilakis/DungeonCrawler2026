using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PGS.UI;

namespace PGS
{
    public class CharacterSelectView : GameView<CharacterSelectState>
    {
		[SerializeField] private ButtonHandler btnAddNewCharacter;
		[SerializeField] private TMP_InputField characterNameField;

		[SerializeField] private ButtonHandler btnPlay;
		public ButtonHandler PlayButton {  get { return btnPlay; } }

		[SerializeField] private TextMeshProUGUI totalCharacterCountText;
		private List<Character> totalCharacters = new List<Character>();

		protected override void OnEnable()
		{
			btnAddNewCharacter.OnRelease += AddCharacter;
		}

		protected override void OnDisable()
		{
			btnAddNewCharacter.OnRelease -= AddCharacter;
		}

		private void AddCharacter()
		{
			totalCharacters.Add(new Character(characterNameField.text));
			totalCharacterCountText.text = $"Total Characters: {totalCharacters.Count.ToString()}";
			characterNameField.text = string.Empty;
		}
	}
}
