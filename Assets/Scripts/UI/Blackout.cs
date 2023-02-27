using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] [Range(0,1)] private float maxA = 1f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float delayOnBlack =0.1f;

    private void Awake()
    {
        var color = image.color;
        color.a = 1;
        image.color = color;
        StartBlackoutCycle(()=>{});
    }

    public bool Started { get; private set;  } = false;
    public void StartBlackoutCycle(Action callbackOnBlack, float delay = -1)
    {
        if(Started == false)
            StartCoroutine(DoBlackoutCycle(callbackOnBlack, delay));
    }

    private IEnumerator DoBlackoutCycle(Action callbackOnBlack, float delay = -1)
    {
        Started = true;
        image.raycastTarget = true;
        if (delay < 0)
            delay = delayOnBlack;
        while (image.color.a < maxA)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime * speed);
            yield return null;
        }
        yield return null;
        callbackOnBlack();
        yield return null;
        yield return new WaitForSeconds(delay);
        while (image.color.a > 0)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - Time.deltaTime * speed);
            yield return null;
        }
        image.raycastTarget = false;
        Started = false;
    }

    public void Black(Action callback)
    {
        if (Started == false)
            StartCoroutine(DoBlack(callback));
    }

    private IEnumerator DoBlack(Action callback)
    {
        Started = true;
        image.raycastTarget = true;
        while (image.color.a < maxA)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + Time.deltaTime * speed);
            yield return null;
        }
        yield return null;
        callback();
        image.raycastTarget = false;
        Started = false;
    }
}
