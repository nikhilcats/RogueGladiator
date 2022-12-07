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

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

}
