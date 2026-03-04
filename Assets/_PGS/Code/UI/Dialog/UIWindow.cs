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

        [Header("Show")]
        [SerializeField] private WindowOpenMode openMode;
        public WindowOpenMode OpenMode { get { return openMode; } }
        [SerializeField] private ClipTransition introAnimation;

        [Header("Hide")]
		[SerializeField] private ClipTransition outroAnimation;

        public string WindowID { get; private set; }

        /// <summary>
        /// Event that's fired when this window wants to spawn; registers itself to and returns the reference of a UIManager for control later
        /// </summary>
        public static Func<UIWindow, string> RegisterWindow;
        public static Action<string> UnregisterWindow;

        public Action<string> OnWindowRequestOpen;
        public Action<string> OnWindowRequestClose;

        protected virtual void Start()
        {
			WindowID = RegisterWindow?.Invoke(this);
		}

        protected virtual void OnDestroy()
        {
            if (WindowID == null) { return; }
			UnregisterWindow?.Invoke(WindowID);
		}

        public virtual async UniTask Show()
        {
            this.gameObject.SetActive(true);
            //Play any intro animations
		}

        public virtual async UniTaskVoid Hide()
        {
            //Play any outro animations
            this.gameObject.SetActive(false);
		}
	}
}
