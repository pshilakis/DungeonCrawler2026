using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PGS.Utilities
{
	/// <summary>
	/// Generic, reuseable helpful functions not specific to any one game system
	/// </summary>
	public static class CommonUtilities
	{
		public static GameObject CreateNewGameObject(string name, Transform parent = null, bool resetTransform = true)
		{
			GameObject go = new GameObject();
			go.name = name;

			SetNewParent(go, parent, -1, resetTransform);
			return go;
		}

		/// <summary>
		/// Gets an existing component or creates one if it doesn't exist
		/// </summary>
		public static T GetOrAddComponent<T>(GameObject obj) where T : Component
		{
			if (!obj.TryGetComponent<T>(out T component))
			{
				return obj.AddComponent<T>();
			}

			return component;
		}

		/// <summary>
		/// Convert one class to another to check if they are compatible
		/// </summary>
		/// <typeparam name="F">The type to convert from</typeparam>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="from">The class to convert</param>
		/// <returns>Whether the conversion was successful</returns>
		public static bool IsConvertable<F,T>(F from)
			where F : class
			where T : class
		{
			return from is T;
		}

		/// <summary>
		/// Convert one class to another to check if they are compatible
		/// </summary>
		/// <typeparam name="F">The type to convert from</typeparam>
		/// <typeparam name="T">The type to convert to</typeparam>
		/// <param name="from">The class to convert</param>
		/// <param name="to">The new class to return</param>
		/// <returns>Whether the conversion was successful</returns>
		public static bool IsConvertable<F,T>(F from, out T to)
			where F : class
			where T : class
		{
			to = from as T;
			return to != null;
		}

		/// <summary>
		/// Resets a given transform back to localPosition (0,0,0), rotation to (0,0,0), and localScale to (1,1,1)
		/// </summary>
		public static void ResetTransform(Transform transform)
		{
			transform.localPosition = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.localRotation = Quaternion.identity;
		}

		public static void SetNewParent(GameObject objectToReparent, Component newParent, int index = -1, bool resetTansform = true)
		{
			Transform transformToReparent = objectToReparent.transform;
			SetNewParent(transformToReparent, newParent, index, resetTansform);
		}

		public static void SetNewParent(Component objectToReparent, Component newParent, int index = -1, bool resetTransform = true)
		{
			Transform transformToReparent = objectToReparent.transform;
			Transform newParentTransform = newParent.transform;
			SetNewParent(transformToReparent, newParentTransform, index, resetTransform);
		}

		public static void SetNewParent(Component objectToReparent, Transform newParent, int index = -1, bool resetTransform = true)
		{
			Transform transformToReparent = objectToReparent.transform;
			SetNewParent(transformToReparent, newParent, index, resetTransform);
		}

		public static void SetNewParent(Transform objectToReparent, Transform newParent, int index = -1, bool resetTransform = true)
		{
			if (objectToReparent == newParent) { return; } //We can't parent a transform to itself
			objectToReparent.parent = newParent;

			if (newParent != null)
			{
				if (index > -1) //if the index is -1, then we want to add it to the end of the sibling index which is the default unity behavior.
				{
					objectToReparent.SetSiblingIndex(Mathf.Clamp(index, 0, newParent.childCount - 1));
				}	
			}

			if (resetTransform)
			{
				ResetTransform(objectToReparent);
			}
		}

		public static void SetNewParent(Transform transformToReparent, Transform newParent)
		{
			transformToReparent.parent = newParent;
			ResetTransform(transformToReparent);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static bool DestroyCurrent<T>(T obj) where T : Object
		{
			if (obj == null) { return false; } //if the component is null, we can't do anything 

			if (Application.isPlaying) //If the game is running, Unity docs say to use Destroy()
			{
				Object.Destroy(obj);
			}
			else //if we're not playing and using an editor script, then we need to use DestroyImmediate(), but the console will log this as an error saying you shouldn't use DestroyImmediate() in OnValidate()
			{
				//This fixes that issue
				UnityEditor.EditorApplication.delayCall += () =>
				{
					Object.DestroyImmediate(obj);
				};
			}

			return true;
		}

		/// <summary>
		/// Check child objects for a component of a specific type and output it if it exists
		/// </summary>
		public static bool TryGetComponentInChildren<T>(GameObject obj, out T component) where T : class
		{
			component = obj.GetComponentInChildren<T>();

			if (component != null)
			{
				return true;
			}

			return false;
		}

		public static bool TryGetComponentInChildren<T>(Component obj, out T component) where T : class
		{
			return TryGetComponentInChildren(obj.gameObject, out component);
		}

		/// <summary>
		/// Check parent objects for a component of a specific type and output it if it exists
		/// </summary>
		public static bool TryGetComponentInParent<T>(GameObject obj, out T component) where T : class
		{
			component = obj.GetComponentInParent<T>();
			return component != null;
		}

		public static bool TryGetComponentInParent<T>(Component obj, out T component) where T : class
		{
			component = obj.GetComponentInParent<T>();
			return component != null;
		}

		public static bool GetChildByName(Transform root, string name, out Transform found)
		{
			found = root.Find(name);

			if (found != null)
			{
				return true;
			}

			return false;
		}

		public static T AddComponentToNewGameObject<T>(Transform xform, string name) where T : Component
		{
			T component = new GameObject(name).AddComponent<T>();
			Transform t = component.transform;
			t.parent = xform;
			ResetTransform(t);

			return component;
		}

		public static void DeleteAllChildren(Transform parent)
		{
			foreach (Transform child in parent)
			{
				Object.Destroy(child.gameObject);
			}
		}

		public static void DeleteAllChildren(Component component)
		{
			DeleteAllChildren(component.transform);
		}

		public class WeightedRandom<T>
		{
			//https://gamedev.stackexchange.com/questions/162976/how-do-i-create-a-weighted-collection-and-then-pick-a-random-element-from-it

			private struct Entry
			{
				public float accumulatedWeight;
				public T item;
			}

			private List<Entry> entries = new List<Entry>();
			private float accumulatedWeight;
			private System.Random random = new System.Random();

			public void AddEntry(T _item, float _weight)
			{
				accumulatedWeight += _weight;
				entries.Add(new Entry { item = _item, accumulatedWeight = accumulatedWeight });
			}

			public T GetRandom()
			{
				float r = (float)random.NextDouble() * accumulatedWeight;

				foreach (Entry entry in entries)
				{
					if (entry.accumulatedWeight >= r)
					{
						return entry.item;
					}
				}

				return default(T);
			}
		}

		/// <summary>
		/// Instead of always having to look up how Vector3.sqrMagnitude works every time, just use this
		/// https://www.youtube.com/watch?v=30_spR3cCxw&ab_channel=DaniKrossing
		/// </summary>
		/// <param name="pos1"></param>
		/// <param name="pos2"></param>
		/// <returns></returns>
		public static float GetDistanceSqrMagnitude(Vector3 pos1, Vector3 pos2)
		{
			return (pos1 - pos2).sqrMagnitude;
		}

		/// <summary>
		/// Checks if two vectors are within a given range
		/// </summary>
		/// <param name="pos1"></param>
		/// <param name="pos2"></param>
		/// <param name="range"></param>
		/// <returns></returns>
		public static bool IsWithinRange(Vector3 pos1, Vector3 pos2, float range)
		{
			float distance = GetDistanceSqrMagnitude(pos1, pos2);

			return (distance <= range * range);
		}


	}
}

