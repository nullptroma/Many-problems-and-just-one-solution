using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class SignalInvert : SignalSenderBehaviour
{
    [SerializeField] private SignalSenderBehaviour sender;

    private void Start()
    {
        sender.StateChanged += StateChanged;
    }

    public override bool GetValue()
    {
        return !sender.GetValue();
    }

    public override event EventHandler StateChanged;
}
