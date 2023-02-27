using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]
public class YBounds : MonoBehaviour
{
    [SerializeField] private float maxDist;
    [SerializeField] private float power = 5f;
    [SerializeField] private float stablePower = 0.5f;
    private float _start;
    private Rigidbody2D _rb;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _start = _rb.position.y;
    }

    private void FixedUpdate()
    {
        float curDist = _rb.position.y - _start;
        if (Mathf.Abs(curDist) > maxDist)
            _rb.AddForce(Vector2.down * (curDist * power));
        else
            _rb.AddForce(Vector2.up * (-0.1f * _rb.velocity.y * stablePower));
    }
}
