using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")] 
    [SerializeField] private LayerMask objectMask;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, objectMask))
        {
            Return();
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