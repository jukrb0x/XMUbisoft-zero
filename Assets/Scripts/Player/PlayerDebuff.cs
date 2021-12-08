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
    [SerializeField] private float TimeBtnSound = 1f;
    private float NextTimeMuzzle;
    private float NextTimeSound = 0;
    private bool CanMuzzle;
    private bool CanSound = true;
    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        IfCanMuzzle();
        IfCanSound();
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

            if (CanSound)
            {
                AudioManager.Instance.PlayOneShot(AudioEnum.WaterWalk);
                CanSound = false;
                NextTimeSound = Time.time + TimeBtnSound;
            }
            
            //AudioManager.Instance.Play(AudioEnum.WaterWalk);
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

    private void IfCanSound()
    {
        if (Time.time > NextTimeSound)
        {
            CanSound = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Slow"))
        {
            WaterWalkPS.Clear();
            AudioManager.Instance.Stop(AudioEnum.WaterWalk);
            _characterMovement.ResetSpeed();
        }
    }

    
}
