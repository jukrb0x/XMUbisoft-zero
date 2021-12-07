using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWeapon : CharacterComponents
{
    public static Action OnStartShooting;

    [Header("Weapon Settings")] [SerializeField]
    private Weapon weaponToUse;

    [SerializeField] private Transform weaponHolderPosition;


    // Reference of the Weapon we are using
    public Weapon CurrentWeapon { get; set; }

    // Returns the reference to our Current Weapon Aim
    public WeaponAim WeaponAim { get; set; }

    private int mouseLeftBtn = 0;
    public Weapon SecondaryWeapon { get; set; }
    public Weapon ThirdWeapon { get; set; }
    public int beforeindex = -10;
    public int index = 0;
    public int weaponNum = 1;

    public int modNum = 0;
    //public Weapon SecondaryWeapon { get; set; }
    private bool weapon2Status = false;
    private bool weapon3Status = false;


    protected override void Start()
    {
        base.Start();
        EquipWeapon(weaponToUse, weaponHolderPosition);
    }


    protected override void HandleInput()
    {
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (Input.GetMouseButton(0)) // Fire 1
            {
                BeforeShoot();
            }

            if (Input.GetMouseButtonUp(0)) // Fire 1
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            index += 1;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            index -= 1;
        }

        if (SecondaryWeapon != null && ThirdWeapon == null && !weapon2Status)
        {
            weaponNum = 2;
            weapon2Status = true;
            index = 1;
        }

        if (SecondaryWeapon != null && ThirdWeapon != null && !weapon3Status)
        {
            weaponNum = 3;
            weapon3Status = true;
            index = 2;
        }
        modNum = System.Math.Abs(index % weaponNum);

        if (modNum == 0 && beforeindex != index && SecondaryWeapon != null)
        {
            EquipWeapon(weaponToUse, weaponHolderPosition);
            beforeindex = index;
        }

        if (modNum == 1 && beforeindex != index && SecondaryWeapon != null)
        {
            EquipWeapon(SecondaryWeapon, weaponHolderPosition);
            beforeindex = index;
        }

        if (modNum == 2 && beforeindex != index && SecondaryWeapon != null && ThirdWeapon != null)
        {
            EquipWeapon(ThirdWeapon, weaponHolderPosition);
            beforeindex = index;
        }

        // if (Input.GetKeyDown(KeyCode.Q) && SecondaryWeapon != null)
        // {
        //     EquipWeapon(weaponToUse, weaponHolderPosition);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.E) && SecondaryWeapon != null)
        // {
        //     EquipWeapon(SecondaryWeapon, weaponHolderPosition);
        // }
    }


    public void BeforeShoot()
    {
        if (!CurrentWeapon.CanShoot) return;

        CurrentWeapon.TriggerShot();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (CurrentWeapon.CompareTag("Weapon_Initial"))
            {
                UIManager.Instance.SetUIWeaponAmmo("∞", "∞");
            }
            else
            {
                UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
            }
        }
    }

    // When we stop shooting we stop using our Weapon
    public void StopWeapon()
    {
        if (CurrentWeapon == null) return;

        CurrentWeapon.StopWeapon();
    }

    public void Reload()
    {
        if (CurrentWeapon == null) return;

        CurrentWeapon.Reload();
        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (CurrentWeapon.CompareTag("Weapon_Initial"))
            {
                UIManager.Instance.SetUIWeaponAmmo("∞", "∞");
            }
            else
            {
                UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
            }
        }
            
    }

    public void EquipWeapon(Weapon weapon, Transform weaponPosition)
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.WeaponAmmo.SaveCurrentAmmo();
            CurrentWeapon.WeaponAmmo.SaveMaxAmmo();
            WeaponAim.DestroyReticle(); // Each weapon has its own Reticle component
            Destroy(GameObject.Find("PlayerPool"));
            Destroy(CurrentWeapon.gameObject);
        }

        CurrentWeapon = Instantiate(weapon, weaponPosition.position, weaponPosition.rotation);
        CurrentWeapon.transform.parent = weaponPosition;
        CurrentWeapon.SetOwner(character);
        WeaponAim = CurrentWeapon.GetComponent<WeaponAim>();

        if (character.CharacterType == Character.CharacterTypes.Player)
        {
            if (CurrentWeapon.CompareTag("Weapon_Initial"))
            {
                UIManager.Instance.SetUIWeaponAmmo("∞", "∞");
            }
            else
            {
                UIManager.Instance.SetUIWeaponAmmo(CurrentWeapon.CurrentAmmo.ToString(), CurrentWeapon.CurrentMagazine.ToString());
            }
            UIManager.Instance.SetUIWeaponSprite(CurrentWeapon.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite);
        }
    }
}