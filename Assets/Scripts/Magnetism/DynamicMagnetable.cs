using System.Linq;
using UnityEngine;

public class DynamicMagnetable : MonoBehaviour
{
    [SerializeField] private Magnetable[] _magnetables;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag($"Player"))
        {
            foreach (var magnetable in _magnetables)
            {
                magnetable.gameObject.tag = $"Magnetable";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"Player"))
        {
            foreach (var magnetable in _magnetables)
            {
                magnetable.gameObject.tag = $"Untagged";
            }
        }
    }
}
