using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReturnToPool : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask EnemyMask;
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
        if (CheckLayer(other.gameObject.layer, EnemyMask))
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
