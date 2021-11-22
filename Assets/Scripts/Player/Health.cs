using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health :BaseHealth
{
    [Header("Health")]
    [SerializeField] private float initialHealth = 10f;
    [Header("Shield")]
    [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")]
    [SerializeField] private bool destroyObject;

    private Character character;
    private CharacterController controller;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;

    private bool shieldBroken;

    // Controls the current health of the object    

    // Returns the current health of this character
    public float CurrentShield;

    protected override void Awake()
    {
        base.Awake();
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        CurrentShield = initialShield;
        UIManager.Instance.SetUIStates(Hp, maxHp, CurrentShield, maxShield);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    // Take the amount of damage we pass in parameters
    public void TakeDamage(int damage)
    
    {
        if (Hp <= 0)
        {
            return;
        }

        if (!shieldBroken )
        {
            CurrentShield -= damage;
            UIManager.Instance.SetUIStates(Hp, maxHp, CurrentShield, maxShield);

            if (CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }

        Hp -= damage;
        UIManager.Instance.SetUIStates(Hp, maxHp, CurrentShield, maxShield);

        if (Hp <= 0)
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
        }

        if (destroyObject)
        {
            DestroyObject();
        }
    }

    // Revive this game object    
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
           
        }

        gameObject.SetActive(true);

        Hp = initialHealth;
        CurrentShield = initialShield;

        shieldBroken = false;

        UIManager.Instance.SetUIStates(Hp, maxHp, CurrentShield, maxShield);
    }

    // If destroyObject is selected, we destroy this game object
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
