using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentBase : MonoBehaviour
{
    [Header("Sprite")]
    [SerializeField] private Sprite damagedSprite;
    
    [Header("Settings")]
    [SerializeField] private float initialHealth;
    [SerializeField] private int damage = 1;
    [SerializeField] private bool isDamageable;
        
    private BaseHealth health;  
    private SpriteRenderer spriteRenderer; 

    private void Start()
    {
        health = GetComponent<BaseHealth>();
        health.MaxHealthPoint = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (!isDamageable) return;
        health.Damage(damage);

        if (health.HealthPoint > 0)
        {
            spriteRenderer.sprite = damagedSprite;
        }
        else
        {
            Destroy(gameObject);


        }
    }
}