using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerDebuff : MonoBehaviour
{
    [SerializeField] private float SlowSpeed = 2f;
    [SerializeField] protected ParticleSystem WaterWalkPS;
    private CharacterMovement _characterMovement;
    [SerializeField] private float TimeBtnMuzzle = 2.5f;
    private float NextTimeMuzzle;
    private bool CanMuzzle;
    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        IfCanMuzzle();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Slow"))
        {
            if (CanMuzzle)
            {
                WaterWalkPS.Play();
                
                CanMuzzle = false;
                NextTimeMuzzle = Time.time + TimeBtnMuzzle;
            }
            
            _characterMovement.MoveSpeed = SlowSpeed;
        }
    }
    
    private void IfCanMuzzle()
    {
        if (Time.time > NextTimeMuzzle)
        {
            CanMuzzle = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Slow"))
        {
            WaterWalkPS.Clear();
            _characterMovement.ResetSpeed();
        }
    }
}
