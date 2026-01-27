using Animancer;
using UnityEngine;

namespace PGS
{
    /// <summary>
    /// Identifying Interface for anything that requires a loading screen
    /// </summary>
    public interface IRequireLoadScreen
    {
        //Question: How do I prioritize one state requiring a custom outro and its new state requiring an custom intro? I can only play one of them.
        //Possibly: If CustomOutro == null, play the custom intro of the new state, and if CustomIntro == null, play the CustomOutro of previous state

        /// <summary>
        /// The custom loadscreen animation to play before this begins loading
        /// </summary>
        public ClipTransition CustomIntro { get; }

        /// <summary>
        /// The custom loadscreen animation to play when this is finished loading
        /// </summary>
        public ClipTransition CustomOutro { get; }
	}
}
