using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MEC;
using System;

namespace PGS
{
	/// <summary>
	/// Stores specific GameObjects necessary for scene loading during runtime
	/// </summary>
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Image m_LoadingImage;
        [SerializeField] private TextMeshProUGUI m_LoadingText;
		[SerializeField] private AnimationClip intro; //change this to a Animancer Transition
		[SerializeField] private AnimationClip outro; //change this to a Animancer Transition

		private bool m_IsEnabled;

		private void Awake()
		{
			SceneUtilities.RegisterSceneLoader(this);
			m_IsEnabled = true;
		}

		/// <summary>
		/// Show the loading screen (with animation)
		/// </summary>
		/// <returns></returns>
		public Task Show(bool animated)
		{
			if (animated)
			{
				//play the intro anim
			}
			else
			{ 
				//just enable the screen (on) without animation
			}

			m_IsEnabled = true;

			Debug.Log($"Loading Screen Show @ {DateTime.Now.TimeOfDay}");
			return Task.CompletedTask;
		}

		/// <summary>
		/// Hide the loading screen
		/// </summary>
		/// <returns></returns>
		public Task Hide(bool animated)
		{
			if (!m_IsEnabled) { return Task.CompletedTask; } //if the loading screen isn't enabled, then there's nothing to hide

			if (animated)
			{
				//play the intro anim
			}
			else
			{
				//just enable the screen (on) without animation
			}

			Debug.Log($"Loading Screen Hide @ {DateTime.Now.TimeOfDay}");
			return Task.CompletedTask;
		}
	}
}
