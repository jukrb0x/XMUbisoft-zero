using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private float timeBtwShots = 10.5f;

    [Header("Weapon")] [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30; // TODO
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")] [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 30;

    public Character WeaponOwner { get; set; }


    public WeaponAmmo WeaponAmmo { get; set; }

    public int CurrentAmmo { get; set; }
    public bool UseMagazine => useMagazine;

    public int MagazineSize => magazineSize;

    public bool CanShoot { get; set; }

    // Internal
    private float allowNextShotTime;
    private CharacterController controller;

    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = magazineSize;
    }

    protected virtual void Update()
    {
        WeaponCanShoot();
        RotateWeapon();
    }

    public void TriggerShot()
    {
        StartShooting();
    }

    public void StopWeapon()
    {
        if (useRecoil)
        {
            controller.ApplyRecoil(Vector2.one, 0f);
        }
    }

    private void StartShooting()
    {
        if (useMagazine)
        {
            if (WeaponAmmo == null) return;
            if (WeaponAmmo.CanUseWeapon())
            {
                RequestShoot();
            }
            else
            {
                if (autoReload)
                {
                    Reload();
                }
            }
        }
        else
        {
            RequestShoot();
        }
    }

    protected virtual void RequestShoot()
    {
        allowNextShotTime = Time.time + timeBtwShots;
        if (useRecoil)
        {
            Recoil();
        }

        WeaponAmmo.ConsumeAmmo();
    }

    private void Recoil()
    {
        if (WeaponOwner == null) return;
        controller.ApplyRecoil(WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? Vector2.left : Vector2.right,
            recoilForce);
    }

    protected virtual void WeaponCanShoot()
    {
        if (Time.time > allowNextShotTime)
        {
            CanShoot = true;
            // nextShotTime = Time.time + timeBtwShots;
        }
    }

    public void SetOwner(Character owner)
    {
        WeaponOwner = owner;
        controller = WeaponOwner.GetComponent<CharacterController>();
    }

    public void Reload()
    {
        if (WeaponAmmo != null)
        {
            if (useMagazine)
            {
                WeaponAmmo.RefillAmmo();
            }
        }
    }

    protected virtual void RotateWeapon()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}