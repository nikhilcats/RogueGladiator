using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private Rigidbody2D playerRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = 0;
        float inputY = 0;
        if (Input.GetKey(KeyCode.W))
        {
          inputY = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
          inputX = -1;
        }
        if (Input.GetKey(KeyCode.S))
        {
          inputY = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
          inputX = 1;
        }

        Vector2 movement = new Vector2(inputX, inputY);

        playerRigidbody.AddForce(movement * moveSpeed);
    }
}
