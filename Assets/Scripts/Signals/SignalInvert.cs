using System;
using Interfaces;
using UnityEngine;

public class SignalInvert : SignalSenderBehaviour
{
    public override event EventHandler StateChanged;
    [SerializeField] private SignalSenderBehaviour signaler;

    private void Start()
    {
        signaler.StateChanged += (sender, e)=>StateChanged?.Invoke(this, e);
    }

    public override bool GetValue()
    {
        return !signaler.GetValue();
    }

}
