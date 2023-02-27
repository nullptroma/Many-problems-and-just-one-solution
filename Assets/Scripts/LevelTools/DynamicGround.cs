using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGround : MonoBehaviour
{
    private int _count = 0;
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag($"Ground"))
        {
            _count++;
            tag = $"Ground";
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag($"Ground"))
        {
            _count--;
            if(_count <= 0)
                tag = $"Untagged";
        }
    }
}
