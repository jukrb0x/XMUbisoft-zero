using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Header("Settings")]
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shieldBar;
    [SerializeField] private TextMeshProUGUI currentHealthTMP;
    [SerializeField] private TextMeshProUGUI currentShieldTMP;
    [Header("Weapon")]
    [SerializeField] private TextMeshProUGUI currentAmmoTMP;



    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerMaxShield;
    private float playerCurrentShield;
    private int playerCurrentAmmo;
    private int playerMaxAmmo;


    private void Update()
    {
        InternalUpdate();
    }

    public void UpdateHealth(float currentHealth, float maxHealth, float currentShield, float maxShield)
    {
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
        playerCurrentShield = currentShield;
        playerMaxShield = maxShield;
    }
    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        playerCurrentAmmo = currentAmmo;
        playerMaxAmmo = maxAmmo;
    }


    private void InternalUpdate()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, playerCurrentHealth / playerMaxHealth, 10f * Time.deltaTime);
        currentHealthTMP.text = playerCurrentHealth.ToString() + "/" + playerMaxHealth.ToString();

        shieldBar.fillAmount = Mathf.Lerp(shieldBar.fillAmount, playerCurrentShield / playerMaxShield, 10f * Time.deltaTime);
        currentShieldTMP.text = playerCurrentShield.ToString() + "/" + playerMaxShield.ToString();
        currentAmmoTMP.text = playerCurrentAmmo + " / " + playerMaxAmmo;
    }
}
