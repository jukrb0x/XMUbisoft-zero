using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterComponents
{
    [SerializeField] private float walkSpeed = 6f;

    // A property is a method to store / return a value. In this case, its to controls our current move speed
    public float WalkMoveSpeed { get; set; }

    protected override void Start()
    {
        base.Start();
        WalkMoveSpeed = walkSpeed;
    }

    protected override void HandleAbility()
    {
        base.HandleAbility();
        MoveCharacter();
    }

    // Moves our character by our current speed
    private void MoveCharacter()
    {
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        //If we move in diagonally, e.g pressing A & W together, same 1 unit has been moved
        Vector2 movementNormalized = movement.normalized;

        Vector2 movementSpeed = movementNormalized * WalkMoveSpeed;
        controller.SetMovement(movementSpeed);
    }
}

