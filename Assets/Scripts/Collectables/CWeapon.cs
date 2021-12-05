using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWeapon : Collectables
{
    [SerializeField] private ItemData itemWeaponData;

    protected override void Pick()
    {
        EquipWeapon();
    }

    private void EquipWeapon()
    {
        if (character != null)
        {
            character.GetComponent<CharacterWeapon>().SecondaryWeapon = itemWeaponData.WeaponToEquip;
        }

    }
   
}
