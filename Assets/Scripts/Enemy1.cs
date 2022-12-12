using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MovingObject
{

    public int damage = 4;
    public int health = 6;
    public float moveSpeed = 3f;

    private float enemyName;
    private GameObject player;
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
        animator = GetComponent<Animator>();
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

    public void TakeDamage(int amount)
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