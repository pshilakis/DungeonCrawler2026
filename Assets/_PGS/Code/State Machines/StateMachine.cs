using UnityEngine;

namespace PGS
{
    public abstract class StateMachine<T, StateType> 
        where T : class
        where StateType : State<StateType>
    {
		private StateType m_CurrentState;

        public StateType CurrentState
        {
            get {  return m_CurrentState; }
            protected set { m_CurrentState = value; }
        }

    }
}
