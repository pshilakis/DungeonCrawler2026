using System.Collections.Generic;
using UnityEngine;

namespace PGS.Utilities
{
    public class LayerUtilities
    {
		public struct LayerMaskInfo
		{
			/// <summary>
			/// The int index of a given layer
			/// </summary>
			public readonly int layer;

			/// <summary>
			/// The bitmask of a given layer
			/// </summary>
			public readonly LayerMask mask;

			public LayerMaskInfo(string layerName)
			{
				this.layer = LayerMask.NameToLayer(layerName);
				this.mask = LayerMask.GetMask(layerName);
			}
		}

		public const string DEFAULT_LAYER_NAME = "Default";
		public const string UI_LAYER_NAME = "UI";
		public const string IGNORE_RAYCAST_LAYER_NAME = "Ignore Raycast";

		public static IReadOnlyDictionary<string, LayerMaskInfo> LayerMasks = new Dictionary<string, LayerMaskInfo>
		{
			{ "", new LayerMaskInfo(DEFAULT_LAYER_NAME) },
			{ DEFAULT_LAYER_NAME, new LayerMaskInfo(DEFAULT_LAYER_NAME) },
			{ UI_LAYER_NAME, new LayerMaskInfo(UI_LAYER_NAME) },
			{ IGNORE_RAYCAST_LAYER_NAME, new LayerMaskInfo(IGNORE_RAYCAST_LAYER_NAME) }
		};

		/// <summary>
		/// Applies a tag to a given GameObject
		/// </summary>
		/// <param name="tag"></param>
		/// <param name="obj"></param>
		public static void SetTag(string tag, GameObject obj)
		{
			obj.tag = tag;
		}

		/// <summary>
		/// Returns a layer given a layer name
		/// </summary>
		//public static LayerMask GetLayer(string layerName)
		//{
		//	//return LayerMask.NameToLayer(layerName);
		//	return LayerMasks[layerName].layer;
		//}

		/// <summary>
		/// Gets the name of the selected gameobject's layer
		/// </summary>
		public static string GetLayerName(GameObject obj)
		{
			return obj.layer.ToString();
		}

		/// <summary>
		/// Sets the layer name of the given component's game object to the desired name
		/// </summary>
		public static void SetLayer(Component component, string layerName)
		{
			SetLayer(component.gameObject, layerName);
		}

		/// <summary>
		/// Sets the layer of the selected Gameobject to the desired name
		/// </summary>
		public static void SetLayer(GameObject obj, string layerName)
		{
			obj.layer = LayerMasks[layerName].layer;
		}

		/// <summary>
		/// Returns a LayerMask given a layer name (because this is so damn hard to get straight! != LayerMask.NameToLayer())
		/// </summary>
		public static LayerMask GetLayerMask(string layerName = "")
		{
			//return LayerMask.NameToLayer(layerName); //THIS IS NOT THE SAME THING
			//return LayerMask.GetMask(layerName);
			return LayerMasks[layerName].mask;
		}

		public static LayerMask GetLayerMask(string[] layerNames)
		{
			return LayerMask.GetMask(layerNames);
		}
	}
}
