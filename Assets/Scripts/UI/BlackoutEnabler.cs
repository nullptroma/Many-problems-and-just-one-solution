using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackoutEnabler : MonoBehaviour
{
    [SerializeField] private Blackout bl;
    
    // Start is called before the first frame update
    void Start()
    {
        bl.gameObject.SetActive(true);
        bl.StartBlackoutCycle(()=>{});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
