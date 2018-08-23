using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour {

    Rigidbody beeBody;
    public float upForce;
    float maxPositionY = 20f;
    float minPositionY = 0f;
    float rangePositionY = 2f;
    //public float beeSpeed = 5f;

    private void Awake()
    {
        beeBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        movementUpDown();
    }

    private void movementUpDown()
    {
        beeBody.AddRelativeForce(Vector3.up * upForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (transform.position.y >= (maxPositionY - rangePositionY))
        {
            upForce = 6f;
        } else if (transform.position.y <= minPositionY + rangePositionY)
        {
            upForce = 10f;
        }
    }
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
