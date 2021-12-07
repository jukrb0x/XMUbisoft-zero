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
            Weapon secondWeapon = character.GetComponent<CharacterWeapon>().SecondaryWeapon;
            if (secondWeapon == null)
            {
                character.GetComponent<CharacterWeapon>().SecondaryWeapon = itemWeaponData.WeaponToEquip;
            }
            else
            {
                character.GetComponent<CharacterWeapon>().ThirdWeapon = itemWeaponData.WeaponToEquip;
            }
                
        }

    }
   
}
