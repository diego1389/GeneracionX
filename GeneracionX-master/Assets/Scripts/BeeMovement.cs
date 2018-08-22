using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour {

    Rigidbody beeBody;
    public float upForce;
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
    //// Use this for initialization
    //void Start () {

    //}

    //// Update is called once per frame
    //void Update () {

    //}
}
