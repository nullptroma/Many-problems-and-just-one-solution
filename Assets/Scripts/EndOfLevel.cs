using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfLevel : MonoBehaviour
{
    private LevelLoader _loader;
    
    // Start is called before the first frame update
    void Start()
    {
        _loader = FindObjectOfType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag($"Player"))
            _loader.NextScene();
    }
}
