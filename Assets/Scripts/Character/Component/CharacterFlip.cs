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

    private void FlipToMoveDirection()
    {
        if (controller.CurrentMovement.normalized.magnitude > threshold)
        {
            if (controller.CurrentMovement.normalized.x > 0)
            {
                FaceDirection(1);
            }
            else if (controller.CurrentMovement.normalized.x < 0)
            {
                FaceDirection(-1);
            }
            else
            {
                FaceDirection((int)transform.localScale.x);
            }
            
            
        }
    }

    private void FlipToWeaponDirection()
    {
        if (characterWeapon != null)
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
        }
    }
    
    private void FaceDirection(int newDirection)
    {
        Vector3 localScale = character.CharacterSprite.transform.localScale;
        float originalX = System.Math.Abs(localScale.x);
        float originalY = System.Math.Abs(localScale.y);
        
        if (newDirection > 0)
        { 
            character.CharacterSprite.transform.localScale = new Vector3(originalX,originalY,1);
            FacingRight = true;            
        }
        else
        {
            character.CharacterSprite.transform.localScale = new Vector3(-originalX,originalY,1);
            FacingRight = false;            
        }
    }
}