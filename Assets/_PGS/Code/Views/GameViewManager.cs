using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
    public abstract class GameViewManager : MonoBehaviour 
    {
        public static Action<GameViewManager> OnInitialize;

        //public abstract UniTask Initialize();

        protected virtual void Awake()
        {
			OnInitialize?.Invoke(this);
		}
    }
}
