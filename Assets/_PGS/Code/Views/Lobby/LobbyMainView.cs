using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PGS
{
    public class LobbyMainView : GameView
    {
        [SerializeField] private Button playButton;

		protected override void OnEnable()
		{
			base.OnEnable();
		}

		protected override void OnDisable()
		{
			base.OnDisable();
		}

		public void Test()
		{
			Debug.Log("clicked");
		}
    }
}
