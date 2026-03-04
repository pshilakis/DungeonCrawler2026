using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

namespace PGS
{
	/// <summary>
	/// A SceneState ties a GameState to the in-scene elements (including menus, gameplay elements, etc.) and can send data to their specific state;
	/// </summary>
	/// <typeparam name="T"/>The type of GameState that this is tied to</typeparam>
	public abstract class SceneState<T> : MonoBehaviour, IState
	{
		public UniTask Enter()
		{
			throw new NotImplementedException();
		}

		public UniTask Exit()
		{
			throw new NotImplementedException();
		}
	}
}
