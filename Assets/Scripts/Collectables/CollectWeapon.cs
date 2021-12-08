using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectWeapon : Collectables
{
    [SerializeField] private ItemData itemWeaponData;
    [SerializeField] private int AddProjectileNum = 30;

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
            

            if (secondWeapon != null && secondWeapon.CompareTag(gameObject.tag))
            {
                //Add Ammo
                int maxAmmo = character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine + AddProjectileNum;
                character.GetComponent<CharacterWeapon>().CurrentWeapon.WeaponAmmo.SaveMaxAmmo(maxAmmo);
                character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine = maxAmmo;
                UIManager.Instance.SetUIWeaponAmmo(character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentAmmo.ToString(),character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine.ToString());
            }

            if (thirdWeapon != null && thirdWeapon.CompareTag(gameObject.tag))
            {
                //Add Ammo
                int maxAmmo = character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine + AddProjectileNum;
                character.GetComponent<CharacterWeapon>().CurrentWeapon.WeaponAmmo.SaveMaxAmmo(maxAmmo);
                character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine = maxAmmo;
                UIManager.Instance.SetUIWeaponAmmo(character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentAmmo.ToString(),character.GetComponent<CharacterWeapon>().CurrentWeapon.CurrentMagazine.ToString());
            }
            
            if (secondWeapon == null)
            {
                character.GetComponent<CharacterWeapon>().SecondaryWeapon = itemWeaponData.WeaponToEquip;
            }
            else if(thirdWeapon == null && !secondWeapon.CompareTag(gameObject.tag))
            {
                character.GetComponent<CharacterWeapon>().ThirdWeapon = itemWeaponData.WeaponToEquip;
            }

            AudioManager.Instance.Play(AudioEnum.AK47AndShotGunReload);
        }

    }

}
