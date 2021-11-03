using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHealth : BaseHealth
{
    private NPC npc;
    private new Collider2D collider2D;
    [SerializeField] private GameObject healthBar;
    protected Animator animator;
    
    [SerializeField] private float hurtSpeed = 0.005F;

    protected override void Awake()
    {
        base.Awake();
        npc = GetComponent<NPC>();
        collider2D = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        
    }

    public virtual bool Damage(float damage, float invulnerabilityTime = 0)
    {
        if(!IsDead) 
            healthBar.SetActive(true);
        bool damaged = base.Damage(damage, invulnerabilityTime);
        if (!damaged)
            return false;
        return true;
    }

    protected virtual void Die()
    {
        
    }
}
