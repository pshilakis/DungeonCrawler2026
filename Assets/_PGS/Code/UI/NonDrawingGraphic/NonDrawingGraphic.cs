using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// A concrete subclass of the Unity UI `Graphic` class that just skips drawing.
/// Useful for providing a raycast target without actually drawing anything.

[RequireComponent(typeof(CanvasRenderer))]
public class NonDrawingGraphic : Graphic, IPointerEnterHandler, IPointerExitHandler
{
	#region NonDrawingGraphic specific code (I didn't write this so don't change it!)
	public override void SetMaterialDirty() { return; }
	public override void SetVerticesDirty() { return; }

	/// <summary>
	/// Probably not necessary since the chain of calls `Rebuild()`->`UpdateGeometry()`->`DoMeshGeneration()`->`OnPopulateMesh()` won't happen; so here really just as a fail-safe.
	/// </summary>
	/// <param name="vh"></param>
	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		return;
	}
	#endregion

	/// <summary>
	/// Is a pointer over currently over this graphic?
	/// </summary>
	public bool Hovering { get; private set; }

	public void OnPointerEnter(PointerEventData eventData)
	{
		Hovering = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Hovering = false;
	}

	protected override void OnDisable()
	{
		Hovering = false;
		base.OnDisable();
	}
}