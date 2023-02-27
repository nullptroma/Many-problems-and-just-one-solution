using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndOfLevel : MonoBehaviour
{
    [SerializeField] private float delay;
    private LevelLoader _loader;
    private bool _inDelay = false;

    void Start()
    {
        _loader = FindObjectOfType<LevelLoader>();
    }
    
    protected IEnumerator Delay(float sec, UnityAction action)
    {
        yield return new WaitForSeconds(sec);
        action();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag($"Player") && !_inDelay)
        {
            _inDelay = true;
            StartCoroutine(Delay(delay, () => _loader.NextScene()));
        }
    }
}
