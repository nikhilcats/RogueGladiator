using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MovingObject
{

    public int health = 6;
    public float moveSpeed = 3f;

    private float enemyName;
    private GameObject player;
    private Animator animator;
    private EnemyManager enemyManager;
    private GameManager gameManager;
    public GameObject vomit;
    private bool vomitTurn;
    private float moveDuration = 1.5f;
    private float timer;
    private float stillTimer;
    private float stillDuration = 1.5f;
    private int pointValue = 100;     //how many points this enemy is worth
    public AudioClip blueSlimeTakeDamage;
    public AudioClip blueSlimeAttack;

    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("GameManager/ArenaManager(Clone)/PlayerBounds/Player");
        enemyManager = GameObject.Find("GameManager/ArenaManager(Clone)/Enemyground").GetComponent<EnemyManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        timer = moveDuration;
        stillTimer = stillDuration;
        vomitTurn = true;
    }

    protected override void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (vomitTurn)
            {
                velocity = Vector2.zero;
                this.GetComponent<AudioSource>().volume = 0.1f;
                this.GetComponent<AudioSource>().clip = blueSlimeAttack;
                this.GetComponent<AudioSource>().Play();
                GameObject newVomit = Instantiate(vomit, transform.position, Quaternion.identity);
                newVomit.transform.parent = this.transform.parent;
                Vector2 vomVel = player.transform.position - transform.position;
                vomVel.Normalize();
                newVomit.GetComponent<Rigidbody2D>().velocity = vomVel * 3f;
                vomitTurn = false;
                //Destroy(newVomit, 6.0f);
            }
            else
            {
                if (timer < -stillDuration)
                {
                    timer = moveDuration;
                    vomitTurn = true;
                }
            }
        }
        else
        {
             GoTowardsTarget(player.transform.position);
        }
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
        this.GetComponent<AudioSource>().volume = 0.1f;
        this.GetComponent<AudioSource>().clip = blueSlimeTakeDamage;
        this.GetComponent<AudioSource>().Play();
        health -= amount;
        if (health < 0)
        {
            Debug.Log("It's time to die.");
            animator.Play("blueDeath");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
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
