using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private Animator _animator;
    private bool _pressed = false;
    [SerializeField] private float minWeight = 1f;
    private HashSet<ButtonWeightable> _bws = new HashSet<ButtonWeightable>();
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Step(ButtonWeightable bw)
    {
        Debug.Log($"Step: {bw.weight}, {bw.gameObject.name}");
        _pressed = bw.weight >= minWeight;
        AnimUpdate();
        if (_pressed)
            _bws.Add(bw);
    }

    public void Unstep(ButtonWeightable bw)
    {
        _bws.Remove(bw);
        _pressed=_bws.Count != 0;
        AnimUpdate();
    }

    private void AnimUpdate()
    {
        _animator.SetBool($"Pressed", _pressed);
        if(_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && _pressed)
            _animator.Play("ButtonDown");
    }
}
