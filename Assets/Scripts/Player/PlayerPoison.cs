using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoison : MonoBehaviour
{
    [SerializeField] private float poisonDurationTime = 3f;
    [SerializeField] private float poisonDamage = 0.4f;
    [SerializeField] private float timeBetweenPoison = 1f;

    private PlayerHealth _playerHealth;
    private bool isPoison = false;
    private bool poisonCanTakeDamage = true;
    private float poisonEndTime;
    private float nextDamageTime;
    
    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        PoisonCanTakeDamage();
        TakePoison();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Poison"))
        {
            isPoison = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Poison"))
        {
            isPoison = true;
            poisonEndTime = Time.time + poisonDurationTime;
        }
    }

    private void TakePoison()
    {
        if (isPoison)
        {
            if (Time.time <= poisonEndTime)
            {
                if (poisonCanTakeDamage)
                {
                    _playerHealth.Damage(poisonDamage);
                    poisonCanTakeDamage = false;
                    nextDamageTime = Time.time + timeBetweenPoison;
                }
            }
            else
            {
                isPoison = false;
            }
        }
    }

    private void PoisonCanTakeDamage()
    {
        if (Time.time > nextDamageTime)
        {
            poisonCanTakeDamage = true;
        }
    }


}
