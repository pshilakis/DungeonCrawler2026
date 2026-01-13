using System;
using System.Threading.Tasks;
using UnityEngine;

namespace PGS
{
    public abstract class State<T>
    {
        public abstract Task Enter();
        public abstract Task Exit();
    }
}
