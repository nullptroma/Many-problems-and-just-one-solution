using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MagnetContainer : MonoBehaviour
{
    public float q = 1f;
    private Rigidbody2D _rb;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        var mags = GetComponentsInChildren<Magnetable>();
        foreach (var mag in mags)
        {
            mag.q = q / mags.Length;
            mag.GetComponent<Joint2D>().connectedBody = _rb;
        }
    }
}
