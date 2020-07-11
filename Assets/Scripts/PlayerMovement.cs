using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public XAction CurrentXAction;
    public bool JumpPressed;
    public bool JumpDown;

    [SerializeField] private Transform[] feet;
    [SerializeField] private Transform[] headMarkers;
    [SerializeField] private float maxXSpeed;
    [SerializeField] private float maxYSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float breakAcceleration;
    [SerializeField] private float goAcceleration;
    [SerializeField] private float jumpInhibitionAcceleration;
    [SerializeField] private float groundedRayLength;
    [SerializeField] private float touchingCeilingRayLength;
    [SerializeField] private float maxJumpTime;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpDownBufferTime;
    [SerializeField] private LayerMask groundedMask;

    private new Rigidbody2D rigidbody2D;
    private float jumpTimeLeft;
    private float coyoteTimeLeft;
    private float jumpDownBufferTimeLeft;
    private bool isGrounded;
    private bool isTouchingCeiling;

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
        IsGroundedCheck();
        IsTouchingCeilingCheck();
        CoyoteTimeManagement();
        JumpDownBufferTimeManagement();
        XMovement();
        YMovement();
    }

    private void IsGroundedCheck()
    {
        isGrounded = false;
        foreach (Transform foot in feet)
        {
            if (Physics2D.Raycast(foot.position, Vector2.down, groundedRayLength, groundedMask).transform != null)
                isGrounded = true;
        }
    }

    private void IsTouchingCeilingCheck()
    {
        isTouchingCeiling = false;
        foreach (Transform marker in headMarkers)
        {
            if (Physics2D.Raycast(marker.position, Vector2.up, touchingCeilingRayLength, groundedMask).transform != null)
                isTouchingCeiling = true;
        }
    }

    private void JumpDownBufferTimeManagement()
    {
        if(JumpDown)
        {
            JumpDown = false;
            jumpDownBufferTimeLeft = jumpDownBufferTime;
        }
        else
        {
            jumpDownBufferTimeLeft -= Time.fixedDeltaTime;
            if (jumpDownBufferTimeLeft < 0)
                jumpDownBufferTimeLeft = 0;
        }
    }    

    private void CoyoteTimeManagement()
    {
        if (isGrounded)
            coyoteTimeLeft = coyoteTime;
        else
        {
            coyoteTimeLeft -= Time.fixedDeltaTime;
            if (coyoteTimeLeft < 0)
                coyoteTimeLeft = 0;
        }
    }

    private void XMovement()
    {
        void Brake(ref Vector2 _velocity, float multiplier = 1)
        {
            float speedChangeAbs = breakAcceleration * multiplier * Time.fixedDeltaTime;
            if (Mathf.Abs(_velocity.x) > speedChangeAbs)
                _velocity.x += _velocity.x < 0 ? speedChangeAbs : -speedChangeAbs;
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
                    Brake(ref velocity, 2);
                else
                    velocity.x -= goAcceleration * Time.fixedDeltaTime;
                break;
            case XAction.GoRight:
                if (velocity.x < 0)
                    Brake(ref velocity, 2);
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
            if (JumpPressed && !isTouchingCeiling)
            {
                velocity.y = jumpSpeed;
                jumpTimeLeft -= Time.fixedDeltaTime;
                if (jumpTimeLeft < 0)
                    jumpTimeLeft = 0;
            }
            else
            {
                jumpTimeLeft = 0;
                if (velocity.y > 0)
                    velocity.y -= jumpInhibitionAcceleration * Time.fixedDeltaTime;
            }
        }
        else
        {
            if (velocity.y > 0)
                velocity.y -= jumpInhibitionAcceleration * Time.fixedDeltaTime;
            if (jumpDownBufferTimeLeft > 0)
            {
                if (isGrounded || coyoteTimeLeft > 0)
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
