using Animancer;
using Cysharp.Threading.Tasks;
using PGS.UI;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class UIWindow : MonoBehaviour
    {
        public enum WindowOpenMode
        {
            SINGLE, //close all other dialogs on open
            ADDITIVE //add on top of any other dialogs that are currently open
        }

        //public enum DialogType
        //{
        //    MODAL, //takes focus and blocks all other elements from being interacted with
        //    NON_MODAL //does not take focus, for things like menus etc. that can be toggled independently as needed
        //}

        //[SerializeField] private DialogType type;

        [Header("Show")]
        [SerializeField] private WindowOpenMode openMode;
        public WindowOpenMode OpenMode { get { return openMode; } }
        [SerializeField] private ClipTransition introAnimation;

        [Header("Hide")]
        [SerializeField] private ButtonHandler closeButton;
		[SerializeField] private ClipTransition outroAnimation;

        private bool m_Registered = false;

        private UIManager m_UIManager;

        /// <summary>
        /// Event that's fired when this window wants to spawn; registers itself to and returns the reference of a UIManager for control later
        /// </summary>
        public static Func<UIWindow, UIManager> OnUIManagerRequest;
        public static Action<UIWindow> OnWindowRequestHide;

        protected virtual void Start()
        {
			m_UIManager = OnUIManagerRequest?.Invoke(this);
		}

		protected virtual void OnEnable()
		{
            RegisterCloseButton();
		}

		protected virtual void OnDisable()
		{
            UnregisterCloseButton();
		}

        private void RegisterCloseButton()
        {
			if (closeButton != null)
			{
				closeButton.OnRelease += UniTask.Action(Hide);

                m_Registered = true;
			}
            else
            {
                Debug.LogWarning($"{this} does not have a registered close button, and therefore cannot be closed by the player. Double check this is correct.", this.gameObject);
                m_Registered = false;
            }
		}

        private void UnregisterCloseButton()
        {
            if (m_Registered)
            {
				closeButton.OnRelease -= UniTask.Action(Hide);
			}

            m_Registered = false;
        }

        public async UniTask Show()
        {
			
			//Play any intro animations
		}

        public async UniTaskVoid Hide()
        {
			OnWindowRequestHide?.Invoke(this);
            //Play any outro animations
		}
	}
}
