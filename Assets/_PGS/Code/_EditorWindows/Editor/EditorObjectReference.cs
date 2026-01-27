using UnityEditor;
using UnityEngine;

/// <summary>
/// Class to store objects from the Project View along with their GUIDs so that I don't need to use dictionaries or tuples to store this info. Used in EditorTools only
/// </summary>
public class EditorObjectReference
{
	private string guid;
	public string GUID { get { return guid; } }

	private Object asset;
	public Object Asset { get { return asset; } }

	public string Path { get { return AssetDatabase.GetAssetPath(asset); } }

	public EditorObjectReference(string guid, Object asset)
	{
		this.guid = guid;
		this.asset = asset;
	}

	public EditorObjectReference(string guidOrPath, bool isGUID = true)
	{
		if (isGUID)
		{
			this.guid = guidOrPath;
		}
		else
		{
			this.guid = AssetDatabase.AssetPathToGUID(guidOrPath);
		}
		
		string path = AssetDatabase.GUIDToAssetPath(guid);
		this.asset = AssetDatabase.LoadAssetAtPath<Object>(path);
	}

	public EditorObjectReference(Object asset)
	{
		string path = AssetDatabase.GetAssetPath(asset);
		this.guid = AssetDatabase.GUIDFromAssetPath(path).ToString();
		this.asset = asset;
	}
}

