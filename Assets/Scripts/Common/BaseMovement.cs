using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// For GameObjects that are able to move in a specified direction and speed.
/// 
/// This script allows movement of GameObjects to be set through intuitive mathematical direction and speed
/// properties, without messing with physics!
/// 
/// For this to work, Movable2Ds must have a Rigidbody2D component.
public class BaseMovement : MonoBehaviour
{
    private const float floatThreshold = 0.0001F;

	/**
	 * Internally cached direction the GameObject is moving in case the object is not moving. If you want to
	 * get the direction, use the Direction property.
	 */
	private float direction;
	/** Used internally to determine whether the GameObject is moving backwards. */
	private bool isMovingBackwards;

	protected Rigidbody2D body;

	// Start is called before the first frame update
	protected virtual void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	/// <summary>
	/// Speed property. The speed the GameObject is moving in units per second, negative means backwards.<br/>
	/// 
	/// Changing Speed scales HSpeed and VSpeed, but does not change Direction.
	/// </summary>
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
			float vx = value * Mathf.Cos(Direction * Mathf.Deg2Rad), vy = value * Mathf.Sin(Direction * Mathf.Deg2Rad); //Final velocity
			SetVelocity(vx, vy);
		}
	}

	/// <summary>
	/// Direction property. The direction this GameObject is moving, in degrees. Ranges between (-180, 180], 0 means
	/// to the right and increases anti-clockwise.<br/>
	/// 
	/// Changing Direction changes HSpeed and VSpeed, but does not change Speed.
	/// </summary>
	public float Direction
	{
		get
		{
			/* Direction is calculated using its RigidBody2D velocity. If velocity is 0, returns previously cached
			   direction */
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
			SetVelocity(Speed * Mathf.Cos(direction*Mathf.Deg2Rad), Speed * Mathf.Sin(direction*Mathf.Deg2Rad));
		}
	}

	/// <summary>
	/// HSpeed property. Refers to the horizontal speed this GameObject is moving in units per second. Positive means
	/// to the right, and negative means to the left.<br/>
	/// 
	/// Changing HSpeed changes Direction and Speed, but does not change VSpeed.
	/// </summary>
	public float HSpeed
	{
		get => body.velocity.x;
		set => SetVelocity(value, VSpeed);
	}

	/// <summary>
	/// VSpeed property. Refers to the vertical speed this GameObject is moving. Positive means upwards, and negative
	/// means downwards.<br/>
	/// 
	/// Changing VSpeed changes Direction and Speed, but does not change HSpeed.
	/// </summary>
	public float VSpeed
	{
		get => body.velocity.y;
		set => SetVelocity(HSpeed, value);
	}

	/// <summary>
	/// Sets the velocity of this GameObject, in units per second.
	/// </summary>
	/// 
	/// <param name="vx">The horizontal speed</param>
	/// <param name="vy">The vertical speed</param>
	public void SetVelocity(float vx, float vy)
	{
		float dMomentumX = body.mass * (vx - body.velocity.x);
		float dMomentumY = body.mass * (vy - body.velocity.y);
		body.AddForce(new Vector2(dMomentumX, dMomentumY), ForceMode2D.Impulse);
	}
}
