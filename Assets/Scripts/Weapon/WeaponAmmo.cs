using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{   
    private Weapon weapon;    
    
    private void Start()
    {
        weapon = GetComponent<Weapon>(); 
        RefillAmmo();      
    }

    public void ConsumeAmmo()
    {
        if (weapon.UseMagazine)
        {
            weapon.CurrentAmmo -= 1;
        }
    }
    
    public bool CanUseWeapon()
    {
        return weapon.CurrentAmmo > 0;
    }
    
    public void RefillAmmo()
    {
        // TODO: refill time
        if (weapon.UseMagazine)
        {
            weapon.CurrentAmmo = weapon.MagazineSize;
        }
    }
}