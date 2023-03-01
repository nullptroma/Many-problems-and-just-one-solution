using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagnetContainer : MonoBehaviour
{
    public float q = 1f;
    private float curQ = 1f;
    private Rigidbody2D _rb;
    private Magnetable[] _mags;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _mags = GetComponentsInChildren<Magnetable>();
        UpdateQ();
    }

    private void UpdateQ()
    {
        curQ = q;
        foreach (var mag in _mags)
        {
            mag.q = q / _mags.Length;
            mag.GetComponent<Joint2D>().connectedBody = _rb;
        }
    }

    private void Update()
    {
        if (q!=curQ)
        {
            UpdateQ();
        }
    }
}
