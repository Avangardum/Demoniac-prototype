using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public XAction CurrentXAction;

    [SerializeField] private float maxXSpeed;
    [SerializeField] private float maxYSpeed;
    [SerializeField] private float breakAcceleration;
    [SerializeField] private float goAcceleration;

    private new Rigidbody2D rigidbody2D;

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
        void Brake(ref Vector2 _velocity)
        {
            if (Mathf.Abs(_velocity.x) > breakAcceleration * Time.fixedDeltaTime)
                _velocity.x += _velocity.x < 0 ? breakAcceleration * Time.fixedDeltaTime : -breakAcceleration * Time.fixedDeltaTime;
            else
                _velocity.x = 0;
        }

        Vector2 velocity = rigidbody2D.velocity;
        switch(CurrentXAction)
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
        if (velocity.y > maxYSpeed)
            velocity.y = maxYSpeed;
        if (velocity.y < -maxYSpeed)
            velocity.y = -maxYSpeed;
        rigidbody2D.velocity = velocity;
    }
}
