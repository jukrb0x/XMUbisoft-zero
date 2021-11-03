using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlip : CharacterComponents
{
    public enum FlipMode
    {
        MovementDirection,
        WeaponDirection
    }

    [SerializeField] private FlipMode flipMode = FlipMode.MovementDirection;
    [SerializeField] private float threshold = 0.1f; 

    // Returns if our character is facing Right
    public bool FacingRight { get; set; }

    private void Awake()
    {
        FacingRight = true;
    }      

    protected override void HandleAbility()
    {
        base.HandleAbility();
        
        if (flipMode == FlipMode.MovementDirection)
        {
            FlipToMoveDirection();
        }
        else
        {
            FlipToWeaponDirection();
        }
    }

    // Flips our character by the direction we are moving
    private void FlipToMoveDirection()
    {
        if (controller.CurrentMovement.normalized.magnitude > threshold)
        {
            if (controller.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else if(controller.CurrentMovement.normalized.x < 0)
            {
                FaceDirection(-1);
            }
        }
    }

    // Flips our character by our Weapon Aiming
    private void FlipToWeaponDirection()
    {
        /*if (characterWeapon != null)
        {
            float weaponAngle = characterWeapon.WeaponAim.CurrentAimAngleAbsolute;
            if (weaponAngle > 90 || weaponAngle < -90)
            {
                FaceDirection(-1);
            }
            else
            {
                FaceDirection(1);
            }
        }*/
    }

    // Makes our character face the direction in which is moving
    private void FaceDirection(int newDirection)
    {
        if (newDirection == 1)
        { 
            character.CharacterSprite.transform.localScale = new Vector3(1,1,1);
            FacingRight = true;            
        }
        else
        {
            character.CharacterSprite.transform.localScale = new Vector3(-1,1,1);
            FacingRight = false;            
        }
    }
}