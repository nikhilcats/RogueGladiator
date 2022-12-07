using UnityEngine;
using System.Collections;
using System;

public class Player: MovingObject
{
    private float moveSpeed;

    protected override void Start()
    {
        moveSpeed = 5f;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        velocity *= moveSpeed;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }
}