using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private LayerMask WallMask;
    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private LayerMask BossMask;
    [SerializeField] private float lifeTime = 2f;
    
    [Header("Effects")]
    [SerializeField] private ParticleSystem impactPS;


    private Projectile projectile;    

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    // Returns this object to the pool
    private void Return()
    {
        if (projectile != null)
        {
            projectile.ResetProjectile();
        }  
      
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, WallMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }
            impactPS.Play();
            AudioManager.Instance.PlayOneShot(AudioEnum.ProjectileHitWall);
            Invoke(nameof(Return), impactPS.main.duration);

        }

        if (CheckLayer(other.gameObject.layer, EnemyMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }

            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
        
        if (CheckLayer(other.gameObject.layer, BossMask))
        {
            if (projectile != null)
            {
                projectile.DisableProjectile();
            }

            impactPS.Play();
            Invoke(nameof(Return), impactPS.main.duration);
        }
    }

    private bool CheckLayer(int layer,LayerMask objectMask)
    {
        return ((1 << layer) & objectMask )!= 0;
    }
    
    private void OnEnable()
    {
        Invoke(nameof(Return), lifeTime);       
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}