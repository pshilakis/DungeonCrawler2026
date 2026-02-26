using System;
using System.Collections.Generic;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// UIManager keeps track of all open windows for sequential stacking and closing.
	/// GameViews and GameStates should register to their own UI/controls and send those dialogs here for tracking
	/// </summary>
    public class UIManager : MonoBehaviour
    {
		private Dictionary<string, UIWindow> m_WindowRegistry = new Dictionary<string, UIWindow>();
        [SerializeField] private Stack<UIWindow> m_ActiveWindows = new Stack<UIWindow>();

		private void Awake()
        {
			UIWindow.RegisterWindow += RegisterWindow;
			UIWindow.UnregisterWindow += UnregisterWindow;
		}

		/// <summary>
		/// Registers a window to the UIManager and returns a unique ID for that window. This ID can be used for tracking and management purposes.
		/// </summary>
		/// <param name="window"></param>
		/// <returns></returns>
		private string RegisterWindow(UIWindow window)
		{
			string id = GenerateUniqueID();
			m_WindowRegistry.Add(id, window);
			return id;
		}

		private string GenerateUniqueID()
		{
			return Guid.NewGuid().ToString();
		}

		private void UnregisterWindow(string id)
		{
			m_WindowRegistry.Remove(id);
		}
	}
}
