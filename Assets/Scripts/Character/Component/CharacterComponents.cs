using UnityEngine;

public class CharacterComponents : MonoBehaviour
{
    protected Animator animator;
    protected Character character;
    protected CharacterMovement characterMovement;
    protected CharacterWeapon characterWeapon;

    protected CharacterController controller;
    protected float horizontalInput;
    protected float verticalInput;

    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        character = GetComponent<Character>();
        characterWeapon = GetComponent<CharacterWeapon>();
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        HandleAbility();
    }

    // Main method. Here we put the logic of each ability
    protected virtual void HandleAbility()
    {
        InternalInput();
        HandleInput();
    }

    // Here we get the necessary input we need to perform our actions    
    protected virtual void HandleInput()
    {
        
    }

    // Here get the main input we need to control our character
    protected virtual void InternalInput()
    {
        // FIXME: no body use this code
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
    }
}