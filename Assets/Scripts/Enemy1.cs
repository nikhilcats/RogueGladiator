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
    private EnemyManager enemyManager;
    private GameManager gameManager;
    private int pointValue = 100;     //how many points this enemy is worth

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
        enemyManager = GameObject.Find("GameManager/ArenaManager(Clone)/Enemyground").GetComponent<EnemyManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
            animator.Play("SlimeDeathDown");
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

    public void SlimeDeath()
    {
      //add to kill count
      gameManager.totalMobsKilled++;
      //award points
      gameManager.AddPoints(pointValue);
      //remove slime from list
      enemyManager.enemies.Remove(this.gameObject);
      UnityEngine.GameObject.Destroy(this.gameObject);
    }

}
