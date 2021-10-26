using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterComponents : MonoBehaviour
{
    protected float horizontalInput;
    protected float verticalInput;

    protected CharacterController controller;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        HandleAbility();
    }

    // Main method. Here we put the logic of each ability
    protected virtual void HandleAbility()
    {
        InternalInput();
    }

    // Here we get the necessary input we need to perform our actions    
    protected virtual void HandleInput()
    {

    }

    // Here get the main input we need to control our character
    protected virtual void InternalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }
}

