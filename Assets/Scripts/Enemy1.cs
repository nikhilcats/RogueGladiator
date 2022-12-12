using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MovingObject
{

    private float damage;
    private float health;
    private float moveSpeed;
    private float enemyName;
    private GameObject player;
    private Animator animator;
    private float turnWait;
    public float timeToNextTurn = 3f;
    private bool myTurn = false;
    private float timeBeforeEndMove;
    public float moveDuration = 2f;
    private float atkDist = 2f;
    private float dist;

    protected override void Start()
    {
        moveSpeed = 3f;
        base.Start();
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
        animator = GetComponent<Animator>();
        turnWait = 0f;
        timeBeforeEndMove = moveDuration;
        health = 6f;
    }

    protected override void Update()
    {   
        GoTowardsTarget(player.transform.position);
    }

    private void GoTowardsTarget(Vector3 pos)
    {
        velocity = pos - transform.position;
        velocity.Normalize();
        velocity *= moveSpeed;
        var offset = 90f;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle + offset));
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health < 0)
        {
            Debug.Log("It's time to die.");
            Destroy(GetComponent<Rigidbody2D>());
            animator.Play("SlimeDeathDown");
            GameObject.Destroy(this.gameObject, 2);
        }
    }
}