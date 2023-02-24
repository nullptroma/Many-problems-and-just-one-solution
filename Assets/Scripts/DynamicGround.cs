using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag($"Ground"))
        {
            tag = $"Ground";
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag($"Ground"))
        {
            tag = $"Untagged";
        }
    }
}
