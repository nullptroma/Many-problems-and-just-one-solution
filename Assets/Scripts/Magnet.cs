using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Magnet : MonoBehaviour
{
    public float q = 1f;
    public bool Enabled { get; set; }
    [SerializeField] private Rigidbody2D rb;
    private MagnetStatus _status;
    private readonly List<Magnetable> _magnetables = new List<Magnetable>();
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
    }

    public void ChangeMode()
    {
        _status = _status == MagnetStatus.Attraction ? MagnetStatus.Repulsion : MagnetStatus.Attraction;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Enabled)                                                                                                                                                                                  
        {
            float charge = q * (_status == MagnetStatus.Repulsion ? -1 : 1);
            foreach (var mag in _magnetables)
            {
                float dist = Vector2.Distance(mag.rb.position, rb.position);
                float force = mag.q * charge / dist;
                mag.rb.AddForce((rb.position-mag.rb.position).normalized*force);
                rb.AddForce((mag.rb.position-rb.position).normalized*force);
                Debug.Log(force);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Magnetable"))
            _magnetables.Add(other.GetComponent<Magnetable>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag($"Magnetable"))
            _magnetables.Remove(other.gameObject.GetComponent<Magnetable>());
    }
    
    private enum MagnetStatus
    {
        Attraction,
        Repulsion
    }
}
