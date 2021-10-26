using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponents
{
    [SerializeField] private float walkSpeed = 6f;

    // A property is a method to store / return a value. In this case, its to controls our current move speed
    public float MoveSpeed { get; set; }

    // Internal
    private readonly int movingParamater = Animator.StringToHash("Moving");


    protected override void Start()
    {
        base.Start();
        MoveSpeed = walkSpeed;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        MoveCharacter();
        UpdateAnimations();
    }

    // Moves our character by our current speed
    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        //If we move in diagonally, e.g pressing A & W together, same 1 unit has been moved
        Vector2 movementNormalized = movement.normalized;

        Vector2 movementSpeed = movementNormalized * MoveSpeed;
        controller.SetMovement(movementSpeed);
    }

    // Updates our Idle and Move animation
    private void UpdateAnimations()
    {
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            animator.SetBool(movingParamater, value: true);
        }
        else
        {
            animator.SetBool(movingParamater, value: false);
        }
    }

    // Reset our speed from the run speed to the walk speed
    public void ResetSpeed()
    {
        MoveSpeed = walkSpeed;
    }
}
