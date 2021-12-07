using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : CharacterComponents
{
    [Header("Weapon Settings")] [SerializeField]
    private Weapon weaponToUse;

    [SerializeField] private Transform weaponHolderPosition;

    // Reference of the Weapon we are using
    public Weapon CurrentWeapon { get; set; }

    // Returns the reference to our Current Weapon Aim
    public WeaponAim WeaponAim { get; set; }

    private int mouseLeftBtn = 0;

    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }
    

    protected override void HandleInput()
    {
        base.HandleInput();
        BeforeShoot();
        if (Input.GetMouseButtonUp(mouseLeftBtn)) // If we stop shooting
        {
            StopWeapon();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void BeforeShoot()
    {
        if (!CurrentWeapon.CanShoot) return;

        CurrentWeapon.TriggerShot();
        
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            // FIXME
            // TODO: weapon to UI canvas
            UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
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
            UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
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
            UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
        }
    }
}