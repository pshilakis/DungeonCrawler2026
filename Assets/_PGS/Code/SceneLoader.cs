using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Animancer;
using MEC;

namespace PGS
{
	/// <summary>
	/// Stores specific GameObjects necessary for scene loading during runtime
	/// </summary>
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(AnimancerComponent))]
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject m_OnOffRoot;
        [SerializeField] private Image m_LoadingImage;
        [SerializeField] private TextMeshProUGUI m_LoadingText;

		[Header("Animations")]
		[SerializeField] private ClipTransition intro; //change this to a Animancer Transition
		[SerializeField] private ClipTransition outro; //change this to a Animancer Transition

		private AnimancerComponent m_Animator;

		/// <summary>
		/// Is the loading screen gameobject active in the scene (aka is it possible to play animations and see them)?
		/// </summary>
		public bool Enabled { get { return gameObject.activeInHierarchy && m_OnOffRoot.activeSelf; } }

		private void Awake()
		{
			SceneUtilities.RegisterSceneLoader(this);
			m_Animator = GetComponent<AnimancerComponent>();
		}

		/// <summary>
		/// Show the loading screen (with animation)
		/// </summary>
		/// <returns></returns>
		public IEnumerator<float> Show(bool animated)
		{
			if (Enabled) { yield break; } //if the loading screen isn't enabled, then there's nothing to hide

			m_OnOffRoot.SetActive(true);
			AnimancerState state = m_Animator.Play(intro.Clip);
			state.Time = 0f;
			state.Weight = 1f;

			if (animated)
			{
				//Task.Delay((int)state.Length * 1000); // multiply (int) seconds by 1000 to get milliseconds
				//await Task.Delay((int) * 1000);
				yield return Timing.WaitForSeconds(state.Length);
			}
			else
			{
				state.FinishImmediately(); //Jump to the last frame of the intro animation
				yield return Timing.WaitForOneFrame;
			}


			Debug.Log($"Loading Screen Show @ {DateTime.Now.TimeOfDay}");
		}

		/// <summary>
		/// Hide the loading screen
		/// </summary>
		/// <returns></returns>
		public IEnumerator<float> Hide(bool animated)
		{
			if (!Enabled) { yield break; } //if the loading screen isn't enabled, then there's nothing to hide

			AnimancerState state = m_Animator.Play(outro.Clip); //Play the outro animation
			state.Weight = 1f;
			state.Time = 0f;

			if (animated)
			{
				Debug.Log("animated");
				//Task.Delay((int)state.Length * 1000); // multiply (int) seconds by 1000 to get milliseconds
				//await Task.Delay((int) * 1000);
				yield return Timing.WaitForSeconds(state.Length);
			}
			else
			{
				Debug.Log("instant");
				//state.FinishImmediately(); //Jump to the last frame of the outro animation
				yield return Timing.WaitForOneFrame;
			}

			m_OnOffRoot.SetActive(false);
			Debug.Log($"Loading Screen Hide @ {DateTime.Now.TimeOfDay}");
		}
	}
}
