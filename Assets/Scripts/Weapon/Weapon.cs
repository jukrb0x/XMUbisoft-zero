using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Name")]
    [SerializeField] private string weaponName = "";

    [Header("Settings")] [SerializeField] public float timeBtwShots = 0.5f;

    [Header("Weapon")] 
    [SerializeField] private bool useMagazine = true;
    [SerializeField] private int magazineSizes = 30; 
    [SerializeField] private bool autoReload = true;

    [Header("Recoil")] [SerializeField] private bool useRecoil = true;

    [SerializeField] private int recoilForce = 30;
    
    [Header("Effects")] 
    [SerializeField] protected ParticleSystem muzzlePS;
    public string WeaponName => weaponName;


    // Internal
    public float nextShotTime;
    private CharacterController controller;

    public Character WeaponOwner { get; set; }

    public WeaponAim WeaponAim { get; set; }
    public WeaponAmmo WeaponAmmo { get; set; }

    public int CurrentAmmo { get; set; }
    public bool UseMagazine => useMagazine;

    public int CurrentMagazine { get; set; }

    public bool CanShoot { get; set; }

    protected virtual void Awake()
    {
        WeaponAmmo = GetComponent<WeaponAmmo>();
        CurrentAmmo = WeaponAmmo.LoadCurrentAmmo(weaponName);
        CurrentMagazine = WeaponAmmo.LoadMaxAmmo(weaponName);
        // CurrentAmmo = WeaponAmmo.LoadCurrentAmmo();
        // CurrentMagazine = WeaponAmmo.LoadMaxAmmo();
        // if(gameObject.CompareTag("Weapon_Shot"))
        //     CurrentMagazine = 300;
        // print(WeaponAmmo);
        // print("Test Awake");
        // print("Awake" + CurrentMagazine);
    }

    public virtual void Update()
    {
        WeaponCanShoot();
        RotateWeapon();
    }
    
    public virtual void UseWeapon()
    {
        StartShooting();
    }

    public void TriggerShot()
    {
        StartShooting();
    }

    public void StopWeapon()
    {
        if (useRecoil) controller.ApplyRecoil(Vector2.one, 0f);
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
                    if (autoReload) Reload();
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
        if (!CanShoot) return;

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
                controller.ApplyRecoil(Vector2.left, recoilForce);
            else
                controller.ApplyRecoil(Vector2.right, recoilForce);
        }
    }

    public void WeaponCanShoot()
    {
        // TODO: Fix double shoot
        if (Time.time > nextShotTime) CanShoot = true;
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
            transform.localScale = new Vector3(1, 1, 1);
        else
            transform.localScale = new Vector3(-1, 1, 1);
    }
}