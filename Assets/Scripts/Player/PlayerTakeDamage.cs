using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] private float timeBtwDamage = 0.5f;
    private float nextDamageTime;
    
    private bool PlayerCanTakeDamage = true;

    private Health health;

    private void Awake()
    {
        health = GetComponent<Health>();
    }
    
    protected virtual void Update()
    {
        CanTakeDamage();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Enemy") && PlayerCanTakeDamage)
        {
            health.TakeDamage(1);
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
