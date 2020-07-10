using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float GravityModifier = 1;

    protected new Rigidbody2D rigidbody2D;

    private Vector2 velocity = Vector2.zero;

    private void OnEnable()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        velocity += Physics2D.gravity * GravityModifier * Time.fixedDeltaTime;

        Move(velocity * Time.fixedDeltaTime);
    }

    private void Move(Vector2 movement)
    {
        rigidbody2D.position += movement;
    }
}
