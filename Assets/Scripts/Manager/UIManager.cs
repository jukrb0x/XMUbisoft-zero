using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Image healthBar;

    private Image shieldBar;
    private Image healthBarDelay;
    private Image shieldBarDelay;
    [SerializeField] private float amountDelayRate = 0.0005f;
    [Header("Weapon")]
    [SerializeField] private Image weaponImage;


    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerCurrentShield;
    private float playerMaxShield;

    private int currentAmmo;
    private int totalAmmo;


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

    private void Start()
    {
        // Initialize status bars
        Transform statusBarsContainer = GameObject.Find("StatusBarsContainer").transform;
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

    private void UpdateBars()
    {
        // TODO: Mathf.lerp
        healthBar.fillAmount = playerCurrentHealth / playerMaxHealth;
        if (healthBarDelay.fillAmount > healthBar.fillAmount)
        {
            healthBarDelay.fillAmount -= amountDelayRate;
        }
        else
        {
            healthBarDelay.fillAmount = healthBar.fillAmount;
        }
        shieldBar.fillAmount = playerCurrentShield / playerMaxShield;

        if (shieldBarDelay.fillAmount > shieldBar.fillAmount)
        {
            shieldBarDelay.fillAmount -= amountDelayRate;
        }
        else
        {
            shieldBarDelay.fillAmount = shieldBar.fillAmount;
        }
    }
    public void UpdateWeaponSprite(Sprite weaponSprite)
    {
        weaponImage.sprite = weaponSprite;
        weaponImage.SetNativeSize();
    }


}