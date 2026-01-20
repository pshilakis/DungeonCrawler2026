using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Animancer;
using MEC;
using Cysharp.Threading.Tasks;

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
		[SerializeField] private ClipTransition intro;
		[SerializeField] private ClipTransition outro;

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
		public async UniTask Show(bool animated)
		{
			if (Enabled) { return; } //if the loading screen is already showing, we can't show it again

			AnimancerState state = m_Animator.Play(intro.Clip);
			state.Time = 0f;
			state.Weight = 1f;

			if (!animated)
			{
				state.FinishImmediately();
			}

			await state;

			//Debug.Log($"Loading Screen Show @ {DateTime.Now.TimeOfDay}");
		}

		/// <summary>
		/// Hide the loading screen
		/// </summary>
		/// <returns></returns>
		public async UniTask Hide(bool animated)
		{
			if (!Enabled) { return; } //if the loading screen isn't enabled, then there's nothing to hide

			AnimancerState state = m_Animator.Play(outro.Clip); //Play the outro animation
			state.Weight = 1f;
			state.Time = 0f;

			if (!animated)
			{
				state.FinishImmediately();
			}

			await state;

			//Debug.Log($"Loading Screen Hide @ {DateTime.Now.TimeOfDay}");
		}
	}
}
