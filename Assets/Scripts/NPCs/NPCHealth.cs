using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

public class NPCHealth : BaseHealth
{
    [SerializeField] private float initialHealth = 10f;

    // TODO: find health bar here
    private GameObject healthBar;
    private Image healthBarImage;
    private Image healthBarImageDelay;

    protected override void Awake()
    {
        MaxHealthPoint = initialHealth;
        base.Awake();
        healthBar = GameObject.Find("HealthBarContainer");
        Transform healthBarContainer = GameObject.Find("HealthBarContainer").transform;
        healthBarImage = healthBarContainer.Find("HealthBar").GetComponent<Image>();
        healthBarImageDelay = healthBarContainer.Find("HealthBarDelay").GetComponent<Image>();
        healthBar.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Damage(1);
        }
    }
    private void UpdateHealthBar()
    {
        healthBarImage.fillAmount = HealthPoint / MaxHealthPoint;
    }


    public override void Damage(float damage)

    {
        if (!IsDead)
        {
            healthBar.SetActive(true);
        }

        base.Damage(damage);
        UpdateHealthBar();

        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        DestroyObject();
    }
}