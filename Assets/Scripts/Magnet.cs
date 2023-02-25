using System;
using System.Linq;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public float q = 1f;
    private bool Enabled { get; set; }
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform dot;
    private BoxCollider2D _col;
    private MagnetStatus _status;
    private MagnetableList _ml;

    public void ChangeMode()
    {
        _status = _status == MagnetStatus.Attraction ? MagnetStatus.Repulsion : MagnetStatus.Attraction;
    }

    public void SetEnableMagnet(bool e)
    {
        Enabled = e;
        _col.sharedMaterial.friction = e ? 0.4f : 0;
    }

    private void Start()
    {
        _ml = GetComponentsInChildren<MagnetableList>().Single();
        _col = GetComponentsInChildren<BoxCollider2D>().Single();
    }

    private void FixedUpdate()
    {
        if (Enabled)                                                                                                                                                                                  
        {
            float charge = q * (_status == MagnetStatus.Repulsion ? -1 : 1);
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

    private enum MagnetStatus
    {
        Attraction,
        Repulsion
    }
}
