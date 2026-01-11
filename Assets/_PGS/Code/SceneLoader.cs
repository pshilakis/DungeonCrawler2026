using System.Collections.Generic;
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

		private void Awake()
		{
			SceneUtilities.RegisterSceneLoader(this);
		}

		/// <summary>
		/// Show the loading screen
		/// </summary>
		/// <returns></returns>
		public IEnumerator<float> Show()
		{
			yield return Timing.WaitForSeconds(1);
			Debug.Log($"Loading Screen Show @ {DateTime.Now.TimeOfDay}");
		}

		/// <summary>
		/// Hide the loading screen
		/// </summary>
		/// <returns></returns>
		public IEnumerator<float> Hide()
		{
			yield return Timing.WaitForSeconds(1);
			Debug.Log($"Loading Screen Hide @ {DateTime.Now.TimeOfDay}");
		}
	}
}
