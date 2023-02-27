using System;
using UnityEngine;

namespace Interfaces
{
    public abstract class SignalSenderBehaviour : MonoBehaviour
    {
        public abstract bool GetValue();
        public abstract event EventHandler StateChanged;
    }
}