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
    private Renderer myRender;
  
    

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
            nextDamageTime = Time.time + timeBtwDamage;
        }
     

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("Damage_Spike")||other.CompareTag("Debuff_Poison"))&& PlayerCanTakeDamage)
        {
            _playerHealth.Damage(1);
            PlayerCanTakeDamage = false;
            nextDamageTime = Time.time + timeBtwDamage;
        }
    }

    private void CanTakeDamage()
    {
        if (Time.time > nextDamageTime)
        {
            PlayerCanTakeDamage = true;
        }
    }

}
