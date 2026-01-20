using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PGS.UI;

namespace PGS
{
    public class LobbyMainView : GameView
    {
        [SerializeField] private ButtonHandler playButton;

		protected override void OnEnable()
		{
			base.OnEnable();
			playButton.OnPress += LoadPlayboard;
		}


		protected override void OnDisable()
		{
			playButton.OnPress -= LoadPlayboard;
			base.OnDisable();
		}
		private void LoadPlayboard()
		{
			Debug.Log("PLAY");
		}


    }
}
