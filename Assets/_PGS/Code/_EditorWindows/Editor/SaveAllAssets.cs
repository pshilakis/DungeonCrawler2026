using UnityEngine;
using UnityEditor;

//https://docs.unity3d.com/ScriptReference/MenuItem.html
//https://blog.redbluegames.com/guide-to-extending-unity-editors-menus-b2de47a746db

public class SaveAllAssets
{
	[MenuItem("File/Save Unsaved Assets #&s", priority = 170)]

	public static void SaveAllUnsavedAssets()
	{
		AssetDatabase.SaveAssets();
	}
}