using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [Header("Health")] [SerializeField] private float initialHealth = 10f;
    [Header("Shield")] [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")] [SerializeField] private bool destroyObject;

    private Character character;
    private CharacterController controller;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private CharacterWeapon weapon;
    private GameObject weapons;

    private bool IsShieldBroken;

    // Controls the current health of the object    

    // Returns the current health of this character
    public float CurrentShield;

    protected override void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        weapon = GetComponent<CharacterWeapon>();
        // weapons = GameObject.FindWithTag("Weapon");
        weapons = GameObject.Find("WeaponHolder");
        MaxHealthPoint = initialHealth;
        CurrentShield = initialShield;
        base.Awake();
        UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Damage(1);
        }
    }

    // Take the amount of damage we pass in parameters
    public override void Damage(float damage)
    {
        if (!IsShieldBroken)
        {
            CurrentShield -= damage;
            UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);

            if (CurrentShield <= 0)
            {
                IsShieldBroken = true;
            }

            return;
        }
        base.Damage(damage);
        UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
        if (IsDead)
        {
            Die();
        }
    }

    // Kills the game object
    private void Die()
    {
        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;

            weapon.enabled = false;
            Cursor.visible = true;
            weapons.SetActive(false);
        }

        if (destroyObject)
        {
            DestroyObject();
        }
    }

    // Revive this game object    
    // FIXME: awake --> start
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            controller.enabled = true;

            weapon.enabled = true;
            Cursor.visible = false;
            weapons.SetActive(true);
        }

        gameObject.SetActive(true);

        HealthPoint = initialHealth;
        CurrentShield = initialShield;

        IsShieldBroken = false;

        UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }
}