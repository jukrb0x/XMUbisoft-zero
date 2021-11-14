using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float lifeTime = 2f;

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
    
    private void OnEnable()
    {
        Invoke(nameof(Return), lifeTime);       
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}