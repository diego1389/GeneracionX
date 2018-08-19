using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public Transform Target;
    public float Smoothing = 5f;

    Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - Target.position;
    }

    private void FixedUpdate()
    {
        Vector3 targetCamPosition = Target.position + _offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPosition, Smoothing * Time.deltaTime);
    }
}
