using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PingPong : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform end;
    private Vector2 _targetPos;
    private Vector2 _startPos;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPos = _rb.position;
        _targetPos = end.position;
    }

    void Update()
    {
        _rb.MovePosition(Vector2.MoveTowards(_rb.position, _targetPos, speed * Time.deltaTime));
        if (_rb.position == _targetPos)
            _targetPos = _rb.position == _startPos ? end.position : _startPos;
    }
}
