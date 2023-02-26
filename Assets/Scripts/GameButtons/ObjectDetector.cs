using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetector : MonoBehaviour
{
    [SerializeField] private GameButton button;

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out ButtonWeightable bw))
        {
            button.Step(bw);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out ButtonWeightable bw))
        {
            button.Unstep(bw);
        }
    }
}
