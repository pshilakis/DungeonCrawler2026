using Cysharp.Threading.Tasks;
using PGS.Utilities;
using UnityEngine;

namespace PGS
{
    public class StateMachine<TStateType> where TStateType : IState
    {
		private TStateType m_CurrentState;

        public TStateType CurrentState
        {
            get {  return m_CurrentState; }
            protected set { m_CurrentState = value; }
        }

        public virtual async UniTask SetState(TStateType newState)
        {
            if (newState == null) { return; } //no state
            if (newState.Equals(CurrentState)) { return; } //same state
            TStateType previousState = CurrentState;

			string previousStateName = previousState != null ? previousState.GetType().ToString() : "<color=#ff0000>null</color>";
			Debug.Log($"<color=#00ffcc>{typeof(TStateType)} Change:</color> {previousStateName} > {newState.GetType()}");

            IControlInput input;

            if (previousState != null)
            {
                if (CommonUtilities.IsConvertable<TStateType, IControlInput>(previousState, out input))
                {
					input.DisableInputs();
				}

				await previousState.Exit();
			}

			CurrentState = newState;
            await CurrentState.Enter();

            if (CommonUtilities.IsConvertable<TStateType, IControlInput>(CurrentState, out input))
            {
				input.EnableInputs();
			}
        }
    }
}
