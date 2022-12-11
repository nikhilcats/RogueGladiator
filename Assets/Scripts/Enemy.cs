using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{

    private float damage;
    private float health;
    private float moveSpeed;
    private float enemyName;

    protected override void Start()
    {
        moveSpeed = 5f;
        base.Start();
    }

    protected override void Update()
    {

    }

    protected override void FixedUpdate()
    {

    }

    public void TakeDamage(float amount)
    {
      health -= amount;
      if (health < 0)
      {
        health = 0;
      }
    }
}
