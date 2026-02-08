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

		public static Action<CharacterData> OnNewCharacterCreated;

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
			OnNewCharacterCreated?.Invoke(new CharacterData(characterNameField.text));

			totalCharacterCountText.text = $"Total Characters: {GameManager.Instance.Characters.Count}";
			characterNameField.text = string.Empty; //reset the field text to empty
		}
	}
}
