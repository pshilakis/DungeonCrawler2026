using Cysharp.Threading.Tasks;
using PGS.UI;
using UnityEngine;

namespace PGS
{
    public class UIDialog : UIWindow
    {
		public enum DialogType
		{
			MODAL, //takes focus and blocks all other elements from being interacted with
			NON_MODAL //does not take focus, for things like menus etc. that can be toggled independently as needed
		}

		[SerializeField] private DialogType type;
		[SerializeField] private ButtonHandler closeButton;
		private bool m_Registered = false;

		protected virtual void OnEnable()
		{
			if (closeButton != null)
			{
				closeButton.OnRelease += UniTask.Action(Hide);
			}
			
		}

		protected virtual void OnDisable()
		{
			if (closeButton != null)
			{
				closeButton.OnRelease -= UniTask.Action(Hide);
			}
		}
	}
}
