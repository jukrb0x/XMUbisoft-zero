using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{
    public static Action OnStartShooting;
    [Header("Weapon Settings")] 
    [SerializeField] private Weapon weaponToUse;
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
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (Input.GetMouseButton(0))
            {
                BeforeShoot();
            }

            if (Input.GetMouseButtonUp(0))
            {
                StopWeapon();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }

            // if (Input.GetKeyDown(KeyCode.Alpha1) && SecondaryWeapon != null)
            // {
            //     EquipWeapon(weaponToUse, weaponHolderPosition);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.Alpha2) && SecondaryWeapon != null)
            // {
            //     EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            // }
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