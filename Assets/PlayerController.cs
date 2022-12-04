using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    private PlayerCharacter playerCharacter;
    public Rigidbody2D rb; // The character's rigidbody component

    void Start()
    {
        // Get the character's rigidbody component
        rb = GetComponent<Rigidbody2D>();
        playerCharacter = GetComponent<PlayerCharacter>();
    }

    void Update()
    {
        float movementSpeed = playerCharacter.movementSpeed;
        
        // Read the input from the keyboard or gamepad
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Update the character's velocity based on the input
        Vector2 direction = new Vector2(horizontal, vertical);
        direction = direction.normalized;

        Vector2 velocity = direction * movementSpeed * Time.deltaTime;
        rb.velocity = velocity;
    }

}