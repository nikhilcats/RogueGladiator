using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2D;

    protected Vector2 velocity;

    protected bool changeSprite;

    protected virtual void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        changeSprite = false;
        velocity = new Vector2(0, 0);
    }

    protected virtual void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        if (horizontalInput - velocity.x != 0 || verticalInput - velocity.y != 0)
            changeSprite = true;
        else
            changeSprite = false;
        velocity = new Vector2(horizontalInput, verticalInput);
        // Normalize velocity in subclasses!
    }

    protected virtual void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
