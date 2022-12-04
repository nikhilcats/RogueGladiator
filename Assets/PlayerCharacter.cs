using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public string characterName;

    // Core player stats
    public int currentHealth;
    public int maxHealth;
    public int attackDamage;
    public float attackSpeed;
    public float movementSpeed;
    public int currentExp;
    public int expToNextLevel;
    public Vector2 velocity;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        velocity = rb.velocity;
    }

    // // Function to attack another character
    // public void Attack(PlayerCharacter target)
    // {
    //     // Calculate the amount of damage to deal
    //     int damage = attackDamage;

    //     // Apply the damage to the target's health
    //     target.currentHealth -= attackDamage;

    //     // Print a message to the console
    //     Debug.Log(characterName + " attacked " + target.name + " for " + damage + " damage");
    // }
}
