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
    private Rigidbody2D rb2D;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
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

    public void TakeDamage(int amount, Vector2 knockback)
    {
        health -= amount;
        if (rb2D != null)
            rb2D.AddForce(knockback, ForceMode2D.Impulse);
        if (health < 0)
        {
            Debug.Log("It's time to die.");
            Destroy(GetComponent<Rigidbody2D>());
            animator.Play("SlimeDeathDown");
            GameObject.Destroy(this.gameObject, 2);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.SendMessage("TakeDamage", damage);
        }
    }

    void Attack()
    {
        
    }
}