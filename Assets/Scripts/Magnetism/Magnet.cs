using System;
using System.Linq;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float q = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform dot;
    private BoxCollider2D _col;
    private MagnetMode _mode;
    private MagnetableDetector _ml;
    private bool _enabled = false;

    private void Start()
    {
        _ml = GetComponentsInChildren<MagnetableDetector>().Single();
        _col = GetComponentsInChildren<BoxCollider2D>().Single();
    }
    
    public void ChangeMode()
    {
        _mode = _mode == MagnetMode.Attraction ? MagnetMode.Repulsion : MagnetMode.Attraction;
    }

    public void SetEnableMagnet(bool e)
    {
        _enabled = e;
        _col.sharedMaterial.friction = e ? 0.4f : 0;
    }

    private void FixedUpdate()
    {
        if (_enabled)                                                                                                                                                                                  
        {
            float charge = q * (_mode == MagnetMode.Repulsion ? -1 : 1);
            foreach (var mag in _ml.Magnetables)
            {
                var position = dot.position;
                float dist = Vector2.Distance(mag.rb.position, position);
                float force = mag.q * charge / dist;
                mag.rb.AddForce(((Vector2)position-mag.rb.position).normalized*force);
                rb.AddForce((mag.rb.position-(Vector2)position).normalized*force);
            }
        }
    }

    private enum MagnetMode
    {
        Attraction,
        Repulsion
    }
}
