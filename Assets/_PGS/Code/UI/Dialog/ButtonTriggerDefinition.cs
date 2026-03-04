using PGS.UI;
using System;
using UnityEngine;

namespace PGS
{
    [System.Serializable]
    public class ButtonTriggerDefinition : MonoBehaviour
    {
        [SerializeField] private ButtonHandler trigger;
        [SerializeField] private UIWindow window;

        private void Awake()
        {
            window?.Hide();
        }

		private void OnEnable()
		{
            trigger.OnPress += () => window.Show();
		}

        private void OnDisable()
        {
            trigger.OnPress -= () => window.Show();
		}   
	}
}
