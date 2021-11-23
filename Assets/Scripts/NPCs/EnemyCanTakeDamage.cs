using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanTakeDamage : MonoBehaviour
{
    private NPCHealth npcHealth;

    private void Awake()
    {
        npcHealth = GetComponent<NPCHealth>();
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        
        if (other.collider.CompareTag("Projectile"))
        {
            npcHealth.Damage(1);
        }
    }
    
    
    
    
    
    
}
