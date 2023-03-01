using System;
using System.Linq;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private float q = 1f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform dot;
    [SerializeField] private Animator animator;
    private BoxCollider2D _col;
    public MagnetMode Mode { get; private set; } = MagnetMode.Attraction;
    private MagnetableDetector _ml;
    private bool _enabled = false;

    private void Start()
    {
        _ml = GetComponentsInChildren<MagnetableDetector>().Single();
        _col = GetComponentsInChildren<BoxCollider2D>().Single();
    }
    
    public void SetMode(MagnetMode mode)
    {
        Mode = mode;
    }

    public void SetEnableMagnet(bool e)
    {
        animator.SetBool($"IsWork", e);
        _enabled = e;
        _col.sharedMaterial.friction = e ? 0.4f : 0;
    }

    private void FixedUpdate()
    {
        if (_enabled)                                                                                                                                                                                  
        {
            float charge = q * (Mode == MagnetMode.Repulsion ? -1 : 1);
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

    public enum MagnetMode
    {
        Attraction,
        Repulsion
    }
}
