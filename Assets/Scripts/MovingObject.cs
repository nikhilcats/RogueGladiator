using UnityEngine;
using System.Collections;

//The abstract keyword enables you to create classes and class members that are incomplete and must be implemented in a derived class.
public abstract class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2D;

    protected Vector2 velocity;

    protected virtual void Start()
    {
        Debug.Log("MovingObject.Start()");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        velocity = new Vector2(horizontalInput, verticalInput);
        velocity.Normalize();
    }

    protected virtual void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
