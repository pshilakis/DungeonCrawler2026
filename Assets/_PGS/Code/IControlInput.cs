using UnityEngine;

namespace PGS
{
    /// <summary>
    /// Interface that denotes whether or not a state controls player inputs
    /// </summary>
    public interface IControlInput
    {
        public void EnableInputs();
        public void DisableInputs();
    }
}
