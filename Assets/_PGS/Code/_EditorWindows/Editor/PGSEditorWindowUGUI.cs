using UnityEngine;
using UnityEditor;

/// <summary>
/// Custom class that holds my own commonly used EditorWindow methods
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class PGSEditorWindowUGUI<T> : EditorWindow where T : EditorWindow
{
	/// <summary>
	/// Show custom EditorWindow without a custom title
	/// </summary>
	public static void ShowWindow()
	{
		ShowWindow("");
	}

	/// <summary>
	/// Show custom EditorWindow with a custom title
	/// </summary>
	/// <param name="windowTitle"></param>
	public static void ShowWindow(string windowTitle)
    {
		T window = EditorWindow.GetWindow(typeof(T)) as T;

		if (!string.IsNullOrEmpty(windowTitle)) //if we have a custom window title, set it
		{
			GUIContent content = new GUIContent(windowTitle);
			window.titleContent = content;
		}

		window.Show();
	}

	protected abstract void OnGUI();


}

