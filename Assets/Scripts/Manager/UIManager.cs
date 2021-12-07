using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private int currentAmmo;
    private Image healthBar;
    private Image healthBarDelay;
    [SerializeField] private float healthAmountDelayRate;
    [SerializeField] private float shieldAmountDelayRate;
    [Header("Weapon")] [SerializeField] private Image weaponImage;

    [SerializeField] private GameObject weaponAmmoUI;


    private float playerCurrentHealth;
    private float playerCurrentShield;
    private float playerMaxHealth;
    private float playerMaxShield;

    private Image shieldBar;
    private Image shieldBarDelay;
    private int totalAmmo;

    private void Start()
    {
        // Initialize status bars
        var statusBarsContainer = GameObject.Find("StatusBarsContainer").transform;
        healthBar = statusBarsContainer.Find("HealthBarContainer").Find("HealthBar").GetComponent<Image>();
        shieldBar = statusBarsContainer.Find("ShieldBarContainer").Find("ShieldBar").GetComponent<Image>();
        healthBarDelay = statusBarsContainer.Find("HealthBarContainer").Find("HealthBarDelay").GetComponent<Image>();
        shieldBarDelay = statusBarsContainer.Find("ShieldBarContainer").Find("ShieldBarDelay").GetComponent<Image>();
    }

    private void Update()
    {
        UpdateBars();
    }


    public void SetUIPlayerStates(float hp, float maxHp, float shield,
        float maxShield)
    {
        playerCurrentHealth = hp;
        playerMaxHealth = maxHp;
        playerCurrentShield = shield;
        playerMaxShield = maxShield;
    }

    public void SetUIWeaponAmmo(string current, string magazineSize)
    {
        // // TODO: Weapon Switch
        // currentAmmo = current;
        // totalAmmo = magazineSize;
        weaponAmmoUI.GetComponent<TextMeshProUGUI>().text = current + "/" + magazineSize;
    }

    public void SetUIWeaponSprite(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
        weaponImage.SetNativeSize();
    }

    private void UpdateBars()
    {
        healthBar.fillAmount = playerCurrentHealth / playerMaxHealth;
        
        

        if (healthBarDelay.fillAmount > healthBar.fillAmount)
        {
            healthBarDelay.fillAmount -= healthAmountDelayRate * Time.deltaTime;
        }
        else
        {
            healthBarDelay.fillAmount = healthBar.fillAmount;
        }

        shieldBar.fillAmount = playerCurrentShield / playerMaxShield;

        if (shieldBarDelay.fillAmount > shieldBar.fillAmount)
        {
            shieldBarDelay.fillAmount -= shieldAmountDelayRate * Time.deltaTime;
        }
        else
        {
            shieldBarDelay.fillAmount = shieldBar.fillAmount;
        }
    }
}