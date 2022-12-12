using UnityEngine;
using System.Collections;

public abstract class MovingObject : MonoBehaviour
{
    private Rigidbody2D rb2D;

    protected Vector2 velocity;

    protected virtual void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        velocity = new Vector2(0, 0);
    }

    protected virtual void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        velocity = new Vector2(horizontalInput, verticalInput);
        // Normalize velocity in subclasses!
    }

    protected virtual void FixedUpdate()
    {
        if (rb2D != null)
            rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);
    }
}
