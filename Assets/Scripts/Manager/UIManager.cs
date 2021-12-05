using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private int currentAmmo;
    private Image healthBar;
    private Image healthBarDelay;
    [SerializeField] private float amountDelayRate = 0.0005f;
    [Header("Weapon")]
    [SerializeField] private Image weaponImage;


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
        // TODO FIXME
        healthBar = statusBarsContainer.Find("HealthBarContainer").Find("HealthBar").GetComponent<Image>();
        shieldBar = statusBarsContainer.Find("ShieldBarContainer").Find("ShieldBar").GetComponent<Image>();
        healthBarDelay = statusBarsContainer.Find("HealthBarContainer").Find("HealthBarDelay").GetComponent<Image>();
        shieldBarDelay = statusBarsContainer.Find("ShieldBarContainer").Find("ShieldBarDelay").GetComponent<Image>();
    }

    private void Update()
    {
        UpdateBars();
    }


    public void SetUIStates(float hp, float maxHp, float shield,
        float maxShield)
    {
        playerCurrentHealth = hp;
        playerMaxHealth = maxHp;
        playerCurrentShield = shield;
        playerMaxShield = maxShield;
    }

    public void SetWeapon(int current, int magazineSize)
    {
        // TODO: Weapon Switch
        currentAmmo = current;
        totalAmmo = magazineSize;
    }

    private void UpdateBars()
    {
        // TODO: Mathf.lerp
        healthBar.fillAmount = playerCurrentHealth / playerMaxHealth;
        if (healthBarDelay.fillAmount > healthBar.fillAmount)
            healthBarDelay.fillAmount -= amountDelayRate;
        else
            healthBarDelay.fillAmount = healthBar.fillAmount;
        shieldBar.fillAmount = playerCurrentShield / playerMaxShield;

        if (shieldBarDelay.fillAmount > shieldBar.fillAmount)
            shieldBarDelay.fillAmount -= amountDelayRate;
        else
            shieldBarDelay.fillAmount = shieldBar.fillAmount;
    }
    public void UpdateWeaponSprite(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
        weaponImage.SetNativeSize();
    }


}