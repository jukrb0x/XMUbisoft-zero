using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon weapon;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";

    private void Start()
    {
        weapon = GetComponent<Weapon>();
       // RefillAmmo();
        LoadWeaponMagazineSize();
    }

    public void ConsumeAmmo()
    {
        if (weapon.UseMagazine) weapon.CurrentAmmo -= 1;
    }

    public bool CanUseWeapon()
    {
        return weapon.CurrentAmmo > 0;
    }

    public void RefillAmmo()
    {
        // TODO: refill time
        if (weapon.UseMagazine) weapon.CurrentAmmo = weapon.MagazineSize;

        if (weapon.CompareTag("Weapon_Shot"))
        {
            AudioManager.Instance.Play(AudioEnum.AK47AndShotGunReload);
        }else if (weapon.CompareTag("Weapon_AK47"))
        {
            AudioManager.Instance.Play(AudioEnum.AK47AndShotGunReload);
        }
    }
    public void LoadWeaponMagazineSize()
    {
        int savedAmmo = LoadAmmo();
        weapon.CurrentAmmo = savedAmmo < weapon.MagazineSize ? LoadAmmo() : weapon.MagazineSize;
    }

    public void SaveAmmo()
    {
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.CurrentAmmo);
    }

    public int LoadAmmo()
    {
        return PlayerPrefs.GetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.MagazineSize);
    }


}