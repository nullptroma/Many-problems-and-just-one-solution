using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetableDetector : MonoBehaviour
{
    public readonly HashSet<Magnetable> Magnetables = new HashSet<Magnetable>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Magnetable") && other.gameObject.TryGetComponent(out Magnetable m))
            Magnetables.Add(m);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Magnetable m))
        {
            Magnetables.Remove(m);
        }
    }
}
