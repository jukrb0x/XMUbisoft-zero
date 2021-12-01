using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] private float timeBtwDamage = 0.5f;
    private float nextDamageTime;
    
    private bool PlayerCanTakeDamage = true;

    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }
    
    protected virtual void Update()
    {
        CanTakeDamage();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Enemy") && PlayerCanTakeDamage)
        {
            _playerHealth.Damage(1);
            PlayerCanTakeDamage = false;
        }
    }
    
    protected void CanTakeDamage()
    {
        if (Time.time > nextDamageTime)  
        {
            PlayerCanTakeDamage = true;
            nextDamageTime = Time.time + timeBtwDamage;
        }
    }
}
