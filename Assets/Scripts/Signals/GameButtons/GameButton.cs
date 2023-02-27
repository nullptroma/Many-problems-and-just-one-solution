using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class GameButton : SignalSenderBehaviour
{
    private Animator _animator;
    private bool _pressed;
    public override event EventHandler StateChanged;
    [SerializeField] private float minWeight = 1f;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public override bool GetValue()
    {
        return _pressed;
    }

    private HashSet<ButtonWeightable> _bws = new HashSet<ButtonWeightable>();
    public void Step(ButtonWeightable bw)
    {
        Debug.Log($"Step: {bw.weight}, {bw.gameObject.name}");
        _pressed = bw.weight >= minWeight;
        StateChanged?.Invoke(this, EventArgs.Empty);
        AnimUpdate();
        if (_pressed)
            _bws.Add(bw);
    }

    public void Unstep(ButtonWeightable bw)
    {
        _bws.Remove(bw);
        _pressed=_bws.Count != 0;
        StateChanged?.Invoke(this, EventArgs.Empty);
        AnimUpdate();
    }

    private void AnimUpdate()
    {
        _animator.SetBool($"Pressed", _pressed);
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _pressed)
            _animator.Play("ButtonDown");
    }
}
