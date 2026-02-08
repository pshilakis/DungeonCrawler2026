using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using MEC;

namespace PGS.UI
{
	[RequireComponent(typeof(Button))]
	[RequireComponent(typeof(NonDrawingGraphic))]
	[RequireComponent(typeof(DisableRaycastTargetGraphics))]
	public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
	{
		#region Events
		//Any Button
		public Action OnPress;
		public Action OnHold;
		public Action OnRelease;

		//Left Button
		public Action OnLeftPress;	
		public Action OnLeftHold;
		public Action OnLeftRelease;

		//Right Button
		public Action OnRightPress;
		public Action OnRightHold;
		public Action OnRightRelease;
		#endregion

		private Button _button;
		public Image Image 
		{
			get
			{
				return _button.image;
			}
			set
			{
				_button.image = value;
			}
		}

		#region Hold/Drag Variables
		[SerializeField] private bool _isHoldable;
		protected virtual bool IsHoldable { get => _isHoldable; }

		public bool IsHolding { get; protected set; } = false;
		private CoroutineHandle holdCoroutine;
		private bool _isPressed = false;
		#endregion

		private const float HOLD_THRESHOLD = 0.15f; //will need to play around with this to find just the right value, but this feels good so far; not too sensitive but doesn't feel laggy
		private NonDrawingGraphic _collider; //The collider for the button.

		public bool IsPointerOverButton
		{
			get
			{
				if (_collider == null)
				{
					_collider = GetComponent<NonDrawingGraphic>();
				}

				return _collider.Hovering;
			}
		}

		[SerializeField] protected TextMeshProUGUI buttonText;

		protected virtual void Awake()
		{
			_button = GetComponent<Button>();
		}

		public void SetButtonText(string text)
		{
			buttonText.text = text;
		}

		private IEnumerator<float> CheckHolding(PointerEventData eventData)
		{
			_isPressed = true;
			yield return Timing.WaitForSeconds(HOLD_THRESHOLD);
			if (_isPressed) //if the button is still pressed, then we are holding it down
			{
				HandleButtonHold(eventData);
			}
		}

		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (!IsHoldable)
			{
				HandleButtonPress(eventData);
			}
			else
			{
				holdCoroutine = Timing.RunCoroutine(CheckHolding(eventData));
			}
		}

		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (IsHoldable)
			{
				HandleButtonPress(eventData);
			}

			HandleButtonRelease(eventData);
		}

		/// <summary>
		/// Determines which button was pressed
		/// </summary>
		private void HandleButtonPress(PointerEventData eventData)
		{
			OnPress?.Invoke();

			switch (eventData.button)
			{
				case PointerEventData.InputButton.Left:
					OnLeftPress?.Invoke();
					break;
				case PointerEventData.InputButton.Right:
					OnRightPress?.Invoke();
					break;
			}
		}

		/// <summary>
		/// Determines which button is being held
		/// </summary>
		protected virtual void HandleButtonHold(PointerEventData eventData)
		{
			OnHold?.Invoke();

			switch (eventData.button)
			{
				case PointerEventData.InputButton.Left:
					OnLeftHold?.Invoke();
					break;
				case PointerEventData.InputButton.Right:
					OnRightHold?.Invoke();
					break;
			}

			Debug.Log($"HOLDING: {gameObject}");
			IsHolding = true;
		}

		/// <summary>
		/// Determines which button was released
		/// </summary>
		protected virtual void HandleButtonRelease(PointerEventData eventData)
		{
			OnRelease?.Invoke();
			switch (eventData.button)
			{
				case PointerEventData.InputButton.Left:
					OnLeftRelease?.Invoke();
					break;
				case PointerEventData.InputButton.Right:
					OnRightRelease?.Invoke();
					break;
			}

			Timing.KillCoroutines(holdCoroutine);
			IsHolding = false;
			_isPressed = false;
		}
	}
}