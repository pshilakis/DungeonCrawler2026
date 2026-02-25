using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Stack<UIWindow> m_ActiveWindows = new Stack<UIWindow>();

        private void Awake()
        {
			UIWindow.OnUIManagerRequest += RegisterWindow;
			UIWindow.OnWindowRequestHide += CloseDialog;
		}

		private UIManager RegisterWindow(UIWindow window)
		{
			return this;
		}

		private void CloseDialog(UIWindow window)
		{
			
		}
	}
}
