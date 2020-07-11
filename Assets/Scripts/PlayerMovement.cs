using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public XAction CurrentXAction;
    public bool JumpPressed;

    [SerializeField] private Transform feet;
    [SerializeField] private float maxXSpeed;
    [SerializeField] private float maxYSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float breakAcceleration;
    [SerializeField] private float goAcceleration;
    [SerializeField] private float groundedRayLength;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private LayerMask groundedMask;

    private new Rigidbody2D rigidbody2D;
    private float jumpTimeLeft;

    public enum XAction
    {
        Brake,
        GoLeft,
        GoRight
    }

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        XMovement();
        YMovement();
    }

    private void XMovement()
    {
        void Brake(ref Vector2 _velocity)
        {
            if (Mathf.Abs(_velocity.x) > breakAcceleration * Time.fixedDeltaTime)
                _velocity.x += _velocity.x < 0 ? breakAcceleration * Time.fixedDeltaTime : -breakAcceleration * Time.fixedDeltaTime;
            else
                _velocity.x = 0;
        }

        Vector2 velocity = rigidbody2D.velocity;
        switch (CurrentXAction)
        {
            case XAction.Brake:
                Brake(ref velocity);
                break;
            case XAction.GoLeft:
                if (velocity.x > 0)
                    Brake(ref velocity);
                else
                    velocity.x -= goAcceleration * Time.fixedDeltaTime;
                break;
            case XAction.GoRight:
                if (velocity.x < 0)
                    Brake(ref velocity);
                else
                    velocity.x += goAcceleration * Time.fixedDeltaTime;
                break;
        }
        if (velocity.x > maxXSpeed)
            velocity.x = maxXSpeed;
        if (velocity.x < -maxXSpeed)
            velocity.x = -maxXSpeed;
        rigidbody2D.velocity = velocity;
    }

    private void YMovement()
    {
        Vector2 velocity = rigidbody2D.velocity;
        if (jumpTimeLeft > 0)
        {
            if (JumpPressed)
            {
                velocity.y = jumpSpeed;
                jumpTimeLeft -= Time.fixedDeltaTime;
            }
            else
                jumpTimeLeft = 0;
        }
        else
        {
            if (JumpPressed)
            {
                bool isGrounded = Physics2D.Raycast(feet.position, Vector2.down, groundedRayLength, groundedMask).transform != null;
                if(isGrounded)
                {
                    jumpTimeLeft = maxJumpTime;
                    velocity.y = jumpSpeed;
                }
            }
        }
        if (velocity.y > maxYSpeed)
            velocity.y = maxYSpeed;
        if (velocity.y < -maxYSpeed)
            velocity.y = -maxYSpeed;
        rigidbody2D.velocity = velocity;
    }
}
