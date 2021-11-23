using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UI;

public class NPCHealth : BaseHealth
{
    [SerializeField] private float initialHealth = 10f;

    // TODO: find health bar here
    private GameObject healthBarContainer;
    private Image healthBarImage;
    private Image healthBarImageDelay;

    protected override void Awake()
    {
        MaxHealthPoint = initialHealth;
        base.Awake();
        // find current NPC's health bar
        healthBarContainer = gameObject.transform.Find("HealthBarContainer").gameObject;
        healthBarImage = healthBarContainer.transform.Find("HealthBar").GetComponent<Image>();
        healthBarContainer.SetActive(false);
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
            healthBarContainer.SetActive(true);
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