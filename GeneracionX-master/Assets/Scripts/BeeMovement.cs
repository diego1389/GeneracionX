using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour {

    Rigidbody beeBody;
    public float upForce = 10f;
    public float movementForwardSpeed = 0f;
    float maxPositionY = 20f;
    float minPositionY = 0f;
    float maxPositionZ = -18f;
    float minPositionZ = 10f;

    float rangePosition = 2f;
    bool isUp = false;
    bool isDown = false;
    bool isFront = false;
    bool isBack = false;

    //public float beeSpeed = 5f;

    private void Awake()
    {
        beeBody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        movementUpDown();
        movementForwardBackward();
    }

    private void movementForwardBackward()
    {
        beeBody.AddRelativeForce(Vector3.forward * movementForwardSpeed);
    }

    private void movementUpDown()
    {
        beeBody.AddRelativeForce(Vector3.up * upForce);
    }

    private void OnTriggerEnter(Collider other)
    {
        beeMovement();
    }

    private void beeMovement()
    {
        updateBeePositionStates();
        if (isUp && isBack)
        {
            movementForwardSpeed = 0f;
            upForce = 6f;
        }
        else if (isDown && isBack)
        {
            upForce = 6f;
            movementForwardSpeed = 10f;
            //upForce = 10f;
        }
        else if (isDown && isFront)
        {
            movementForwardSpeed = 0f;
            upForce = 10f;
        }
    }

    private void updateBeePositionStates()
    {
        isUp = transform.position.y >= (maxPositionY - rangePosition);
        isDown = transform.position.y <= (minPositionY + rangePosition);
        isFront = transform.position.z <= (maxPositionZ + rangePosition);
        isBack = transform.position.z >= (minPositionZ - rangePosition); 
    }
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
