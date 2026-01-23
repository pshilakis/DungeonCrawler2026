using System.Threading;
using UnityEngine;

namespace PGS
{
    public class StateMachine<StateType> where StateType : IState
    {
		private StateType m_CurrentState;

        public StateType CurrentState
        {
            get {  return m_CurrentState; }
            protected set { m_CurrentState = value; }
        }
    }
}
