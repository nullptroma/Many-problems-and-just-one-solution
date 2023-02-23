using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class RotateToMouse : MonoBehaviour
{
    [SerializeField] private float maxShift = 0.1f;
    [SerializeField] private Transform from;
    [SerializeField] private Transform subject;
    private Camera _mainCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 difference = _mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ-90);
        if (transform.lossyScale.x < 0)
            transform.localScale= new Vector3(transform.localScale.x*-1, transform.localScale.y, transform.localScale.z);
        var newDot = Vector2.Lerp(from.position, _mainCamera.ScreenToWorldPoint(Input.mousePosition), maxShift);
        subject.position = new Vector3(newDot.x, newDot.y, subject.position.z);
    }
}
