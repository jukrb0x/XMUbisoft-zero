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
            Weapon thirdWeapon = character.GetComponent<CharacterWeapon>().ThirdWeapon;
            
            
            
            if (secondWeapon == null)
            {
                character.GetComponent<CharacterWeapon>().SecondaryWeapon = itemWeaponData.WeaponToEquip;
            }
            else if(thirdWeapon == null)
            {
                character.GetComponent<CharacterWeapon>().ThirdWeapon = itemWeaponData.WeaponToEquip;
            }

        }

    }

}
