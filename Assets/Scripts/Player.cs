using UnityEngine;
using System.Collections;
using System;

public class Player: MovingObject
{
    private float moveSpeed;
    public bool outOfBounds = true;

    protected override void Start()
    {
        //Debug.Log("Player.Start()");
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

    // void OnTriggerExit2D(Collider2D limits)
    // {
    //     outOfBounds = false;
    // }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
    }
}