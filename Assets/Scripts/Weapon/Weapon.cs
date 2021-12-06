using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{   
    [Header("Settings")] 
    [SerializeField] public float timeBtwShots = 0.5f;

    [Header("Weapon")] 
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSize = 30; 
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")] 
    [SerializeField] private bool useRecoil = true;
    [SerializeField] private int recoilForce = 30;
    
    [Header("Effects")] 
    [SerializeField] protected ParticleSystem muzzlePS;
    
    public Character WeaponOwner { get; set; }


    public WeaponAmmo WeaponAmmo { get; set; }
    
    public int CurrentAmmo { get; set; }
    public bool UseMagazine => useMagazine;
    
	public int MagazineSize => magazineSize;
    
    public bool CanShoot { get; set; }

    // Internal
    public float nextShotTime;
    private CharacterController controller; 

    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = magazineSize;
    }

    public virtual void Update()
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
            if (WeaponAmmo != null)
            {
                if (WeaponAmmo.CanUseWeapon())
                {
                    RequestShot();
                    
                }
                else
                {
                    if (autoReload)
                    {
                        Reload();
                    }
                }
            }
        }
        else
        {
            RequestShot();
            
		}
    }

    public virtual void RequestShot()
    {
        if (!CanShoot)
        {
            return;
        }

        if (useRecoil)
        {
            Recoil();
        }
        
        
        // Debug.Log("True");
        WeaponAmmo.ConsumeAmmo();   
        
	}

    private void Recoil()
    {
        if (WeaponOwner != null)
        {
            if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                controller.ApplyRecoil(Vector2.left, recoilForce);
            }
            else
            {
                controller.ApplyRecoil(Vector2.right, recoilForce);
            }
        }
    }

    public void WeaponCanShoot()
    {
        // TODO: Fix double shoot
        if (Time.time > nextShotTime)  
        {
            CanShoot = true;
            
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