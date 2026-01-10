using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using MEC;

namespace PGS
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Canvas m_Canvas;
        [SerializeField] private Image m_LoadingImage;
        [SerializeField] private TextMeshProUGUI m_LoadingText;

		private void Awake()
		{
			SceneUtilities.RegisterSceneLoader(this);
		}
	}
}
