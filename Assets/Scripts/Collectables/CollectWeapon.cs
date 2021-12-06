using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapon : Collectables
{
    [SerializeField] private ItemData itemWeaponData;

    protected override void Collect()
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
