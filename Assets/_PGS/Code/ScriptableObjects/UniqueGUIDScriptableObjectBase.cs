using System;
using UnityEditor;
using UnityEngine;

public class UniqueGUIDScriptableObjectBase : ScriptableObject
{
	[Tooltip("The GUID assigned to this asset by Unity on initial creation")]
	[ReadOnly][SerializeField] private string m_AssetGUID; //sub-assets apparently share an assigned GUID with their parent asset, so we can't use the actual GUID when referencing these types of objects. Still, maybe this will be useful someday so I'll keep it.

	[Tooltip("A custom-generated ID (different from the asset's GUID) that we use to refer to this specific ScriptableObject at runtime")]
	[ReadOnly][SerializeField] private string m_CustomID;

	public string ID
	{
		get
		{
			return m_CustomID;
		}

		private set
		{
			m_CustomID = value;
		}
	}

	private void Reset()
	{
		GenerateID(); //When a new scriptable object is first created, we should auto set the ID so that we don't have to manually go and set it on each asset
	}

	private const string contextMenuName = "ID/";
	/// <summary>
	/// Generate a new, unique ID if none exist currently
	/// </summary>
	[ContextMenu(contextMenuName + "Generate New ID")]
	private void GenerateID()
	{
		if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(this, out string guid, out long localId))
		{
			m_AssetGUID = guid;
		}		

		if (string.IsNullOrEmpty(ID)) //if the itemID is already set, don't modify it
		{
			ID = Guid.NewGuid().ToString();
			Debug.Log($"New {GetType()} asset created.\nGUID: {m_AssetGUID} | CustomID: {ID}", this);
		}
		else
		{
			Debug.LogWarning($"{this.name} already has an ID. A new one can not be set with this function.\nGUID: {m_AssetGUID} | CustomID: {ID}", this);
		}
	}

	/// <summary>
	/// Generate a new, unique ID over an existing one
	/// </summary>
	[ContextMenu(contextMenuName + "Override With New ID")]
	private void OverrideID()
	{
		string oldID = ID;
		ID = Guid.NewGuid().ToString();
		Debug.LogWarning($"{this.name} had its original ID ({oldID}) replaced with a new ID ({ID})");
	}
}
