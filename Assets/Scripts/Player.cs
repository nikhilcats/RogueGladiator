using UnityEngine;
using System.Collections;
using System;
using TMPro;

public class Player: MovingObject
{
    private float moveSpeed;
    private int health;
    private int damage;
    public float knockbackForce = 100f;
    private TextMeshProUGUI healthUIText;
    private GameObject gameManager;
    private GameManager gManagerScript;
    private Vector3 localScale;
    private Animator animator;
    public Transform attackPointDown;
    public Transform attackPointUp;
    public Transform attackPointLeft;
    public Transform attackPointRight;
    public float attackRange = 0.9f;
    public LayerMask enemyLayers;
    private Transform attackPoint;
    private float timeSinceLastHit;
    public AudioClip attackSound;
    public AudioClip takeDamageSound;
    public AudioClip deathSound;
    public AudioClip deathBooSound;

    protected override void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gManagerScript = gameManager.GetComponent<GameManager>();
        health = gManagerScript.playerHealth;
        moveSpeed = gManagerScript.playerMoveSpeed;
        damage = gManagerScript.playerAtkDamage;
        healthUIText = GameObject.Find("GameManager/UICanvas/HPtext").GetComponent<TextMeshProUGUI>();
        animator = this.GetComponent<Animator>();
        updateHealthText();
        base.Start();
        localScale = transform.localScale;
    }

    protected override void Update()
    {
        base.Update();
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", velocity.x);
        animator.SetFloat("Vertical", velocity.y);
        animator.SetFloat("Speed", velocity.sqrMagnitude);  //might change

        if (velocity.x != 0 || velocity.y != 0)
        {
            animator.SetFloat("LastX", velocity.x);
            animator.SetFloat("LastY", velocity.y);
        }

        velocity.Normalize();
        velocity *= moveSpeed;

        if (velocity.x > 0)
        {
            attackPoint = attackPointRight;
        }
        else if (velocity.x < 0)
        {
            attackPoint = attackPointLeft;
        }
        else if (velocity.y > 0)
        {
            attackPoint = attackPointUp;
        }
        else if (velocity.y < 0)
        {
            attackPoint = attackPointDown;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }

        //calculate time for iframes
        timeSinceLastHit += Time.deltaTime;
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        this.GetComponent<AudioSource>().clip = attackSound;
        this.GetComponent<AudioSource>().volume = 0.08f;
        this.GetComponent<AudioSource>().Play();
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            if (enemy.name == "Enemy1(Clone)")
                enemy.gameObject.GetComponent<Enemy1>().TakeDamage(damage);
            if (enemy.name == "Enemy2(Clone)")
                enemy.gameObject.GetComponent<Enemy2>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int amount)
    {
      //i frames
      if (timeSinceLastHit > 0.5f) {
        this.GetComponent<AudioSource>().clip = takeDamageSound;
        this.GetComponent<AudioSource>().volume = 0.2f;
        this.GetComponent<AudioSource>().Play();
        timeSinceLastHit = 0;
        health -= amount;
        if (health <= 0)
        {
          health = 0;
          //player death
         gManagerScript.Die();

        }
        //update game manager player health
        gManagerScript.playerHealth = health;
        updateHealthText();
      }

    }

    void updateHealthText()
    {
      string healthStr = health.ToString();
      healthUIText.text = healthStr;
    }
}
