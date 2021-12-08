using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    private Weapon weapon;
    private readonly string WEAPON_AMMO_SAVELOAD = "Weapon_";
    private readonly string WEAPON_AMMO_MAX_SAVELOAD = "WeaponAmmoMax_";

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
        if (weapon.CompareTag("Weapon_Shot"))
        {
            AudioManager.Instance.Play(AudioEnum.AK47AndShotGunReload);
        }else if (weapon.CompareTag("Weapon_AK47"))
        {
            AudioManager.Instance.Play(AudioEnum.AK47AndShotGunReload);
        }
        if (weapon.UseMagazine)
        {
            if (weapon.CurrentMagazine == 0) return;
            
            int reloadAmmo = 30 - weapon.CurrentAmmo;
            if (weapon.CurrentMagazine - reloadAmmo >= 0)
            {
                weapon.CurrentAmmo += reloadAmmo;
                weapon.CurrentMagazine -= reloadAmmo;    
            }
            else
            {
                weapon.CurrentAmmo += weapon.CurrentMagazine;
                weapon.CurrentMagazine = 0;
            }
            
        }


    }
    public void LoadWeaponMagazineSize()
    {
        int savedAmmo = LoadCurrentAmmo();
        int maxAmmo = LoadMaxAmmo();
        weapon.CurrentAmmo = savedAmmo;
        weapon.CurrentMagazine = maxAmmo;
    }

    public void SaveCurrentAmmo()
    {
        PlayerPrefs.SetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName, weapon.CurrentAmmo);
    }

    public void SaveMaxAmmo(int maxAmmo=-1)
    {
        if (maxAmmo == -1)
        {
            PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD + weapon.WeaponName, weapon.CurrentMagazine);
        }
        else
        {
            PlayerPrefs.SetInt(WEAPON_AMMO_MAX_SAVELOAD+weapon.WeaponName, maxAmmo);
        }

    }

    public int LoadCurrentAmmo(string weaponName = "-1")
    {
        int a = 0;
        if (weaponName == "-1")
        {
            a = PlayerPrefs.GetInt(WEAPON_AMMO_SAVELOAD + weapon.WeaponName);
        }
        else
        {
            a = PlayerPrefs.GetInt(WEAPON_AMMO_SAVELOAD + weaponName);
        }
        return a;
    }

    public int LoadMaxAmmo(string weaponName = "-1")
    {
        int a = 0;
        if (weaponName == "-1")
        {
            a = PlayerPrefs.GetInt(WEAPON_AMMO_MAX_SAVELOAD + weapon.WeaponName);
        }
        else
        {
            a = PlayerPrefs.GetInt(WEAPON_AMMO_MAX_SAVELOAD + weaponName);
        }
        return a;
    }


}