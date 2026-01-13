using UnityEngine;

namespace PGS
{
    public abstract class StateMachine<T, S> 
        where T : class
        where S : State<S>
    {
		[ReadOnly][SerializeReference] protected S m_CurrentState;
    }
}
