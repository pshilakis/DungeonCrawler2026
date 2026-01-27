using Cysharp.Threading.Tasks;
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

			Debug.Log($"<color=#00ffcc>{typeof(TStateType)} Change:</color> {previousState?.GetType()} > {newState.GetType()}");

            if (previousState != null)
            {
                if (previousState is IControlInput)
                {
                    IControlInput input = previousState as IControlInput;
                    input.DisableInputs();
                }

				await previousState.Exit();
			}

			CurrentState = newState;
            await CurrentState.Enter();

            if (CurrentState is IControlInput)
            {
                IControlInput input = CurrentState as IControlInput;
                input.EnableInputs();
            }
        }
    }
}
