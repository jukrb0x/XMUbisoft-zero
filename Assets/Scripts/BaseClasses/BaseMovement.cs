using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    private const float floatThreshold = 0.0001F;

    protected Rigidbody2D body;

    private float direction;

    private bool isMovingBackwards;

    public float Speed
    {
        get
        {
            float vx = body.velocity.x, vy = body.velocity.y;
            return Mathf.Sqrt(vx * vx + vy * vy);
        }
        set
        {
            isMovingBackwards = value < 0;
            float vx = value * Mathf.Cos(Direction * Mathf.Deg2Rad),
                vy = value * Mathf.Sin(Direction * Mathf.Deg2Rad); 
            SetVelocity(vx, vy);
        }
    }

    public float Direction
    {
        get
        {
            if (Mathf.Abs(Speed) > floatThreshold)
                direction = Vector2.SignedAngle(Vector2.right, body.velocity);
            if (isMovingBackwards)
            {
                if (direction > 0)
                    direction -= 180;
                else
                    direction += 180;
            }

            return direction;
        }
        set
        {
            direction = value;
            SetVelocity(Speed * Mathf.Cos(direction * Mathf.Deg2Rad), Speed * Mathf.Sin(direction * Mathf.Deg2Rad));
        }
    }

    public float HSpeed
    {
        get => body.velocity.x;
        set => SetVelocity(value, VSpeed);
    }

    public float VSpeed
    {
        get => body.velocity.y;
        set => SetVelocity(HSpeed, value);
    }

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float vx, float vy)
    {
        var dMomentumX = body.mass * (vx - body.velocity.x);
        var dMomentumY = body.mass * (vy - body.velocity.y);
        body.AddForce(new Vector2(dMomentumX, dMomentumY), ForceMode2D.Impulse);
    }
}