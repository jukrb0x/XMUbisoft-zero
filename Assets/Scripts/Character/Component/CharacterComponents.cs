using System;
using UnityEngine;

public class CharacterComponents : MonoBehaviour
{
    protected Animator animator;
    protected Character character;
    protected CharacterMovement characterMovement;
    protected CharacterWeapon characterWeapon;

    protected CharacterController controller;
    protected LevelManager levelManager;
    protected float horizontalInput;
    protected float verticalInput;
    
    public bool canMove;
    public bool canShoot;


    protected virtual void Start()
    {
        controller = GetComponent<CharacterController>();
        character = GetComponent<Character>();
        characterWeapon = GetComponent<CharacterWeapon>();
        characterMovement = GetComponent<CharacterMovement>();
        animator = GetComponent<Animator>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    protected virtual void Update()
    {
        canMove = levelManager.canMove;
        canShoot = levelManager.canShoot;
        HandleAbility();
    }


    // Main method. Here we put the logic of each ability
    protected virtual void HandleAbility()
    {
        if (!canMove || !canShoot) return;
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