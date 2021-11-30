using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{   	
    [Header("Weapon Settings")]
    [SerializeField] private Weapon weaponToUse;
    [SerializeField] private Transform weaponHolderPosition;

    // Reference of the Weapon we are using
	public Weapon CurrentWeapon  { get; set; }

	// Returns the reference to our Current Weapon Aim
    public WeaponAim WeaponAim { get; set; }

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }
    

    protected override void HandleInput()
    {
        if (Input.GetMouseButton(0))
        {
        	Shoot();
        }

        if (Input.GetMouseButtonUp(0))  // If we stop shooting
        {
        	StopWeapon();    
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
	      Reload();
        }    
    }
    
    public void Shoot()
    {
        if (CurrentWeapon == null)
        {
            return;
        } 

        CurrentWeapon.TriggerShot();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            // FIXME
            UIManager.Instance.SetWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }     
    }

    // When we stop shooting we stop using our Weapon
    public void StopWeapon()
    {
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.StopWeapon();
    }

    public void Reload()
    {         
        if (CurrentWeapon == null)
        {
            return;
        }
        
        CurrentWeapon.Reload();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.SetWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }

    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);     
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>(); 

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            UIManager.Instance.SetWeapon(CurrentWeapon.CurrentAmmo, CurrentWeapon.MagazineSize);
        }
    }
}