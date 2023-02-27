using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipXScale : MonoBehaviour
{
    public float sec = 0.2f;
    public int steps = 10;
    private float _targetScaleX;
    
    public void Flip()
    {
        StopAllCoroutines();
        _targetScaleX *= -1;
        StartCoroutine(FlipCor());
    }

    private IEnumerator FlipCor()
    {
        float step = (_targetScaleX - transform.localScale.x)/steps;
        int count = steps;
        while (count-- > 0)
        {
            var localScale = transform.localScale;
            transform.localScale = new Vector3(localScale.x+step,localScale.y, localScale.z);
            yield return new WaitForSeconds(sec/steps);
        }
        transform.localScale = new Vector3(_targetScaleX,transform.localScale.y, transform.localScale.z);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        _targetScaleX = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
