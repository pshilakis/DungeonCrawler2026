using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using MEC;

namespace PGS.UI
{
	public class DraggableButtonHandler : ButtonHandler
	{
		public Action OnDrag;

		protected override bool IsHoldable { get { return true; } } //this is always true for draggable buttons, no matter what is checked in the inspector (TODO: make a custom inspector that hides this field)

		protected override void HandleButtonHold(PointerEventData eventData)
		{
			base.HandleButtonHold(eventData);
			Timing.RunCoroutine(DragCoroutine());
		}

		private IEnumerator<float> DragCoroutine()
		{
			while (IsHolding)
			{
				OnDrag?.Invoke();
				yield return Timing.WaitForOneFrame;
			}
		}
	}
}

