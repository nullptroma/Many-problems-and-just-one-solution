using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Magnetable : MonoBehaviour
{
    public float q = 1f;
    public Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        tag = "Magnetable";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
