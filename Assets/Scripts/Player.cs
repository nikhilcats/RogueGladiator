using UnityEngine;
using System.Collections;
using System;

public class Player: MovingObject
{
    private float moveSpeed;
    protected override void Start()
    {
        Debug.Log("Player.Start()");
        moveSpeed = 1f;
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
}