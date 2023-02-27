using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator))]
public class Door : SignalReceiverBehaviour
{
    private Animator _animator;
    private bool _isOpen;
    [SerializeField] private SignalSenderBehaviour[] senders;

    void Start()
    {
        _animator = GetComponent<Animator>();
        foreach (var signalSender in senders)
        {
            signalSender.StateChanged += (e, sender) => CheckSenders();
        }
    }

    private void CheckSenders()
    {
        _isOpen = senders.All(s=>s.GetValue());
        UpdateAnim();
    }

    void UpdateAnim()
    {
        _animator.SetBool($"IsOpen", _isOpen);
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _isOpen)
            _animator.Play("Open");
    }
}
