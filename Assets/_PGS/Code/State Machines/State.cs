using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    public abstract class State<T>
    {
        public abstract bool Enter();
        public abstract bool Exit();
    }
}
