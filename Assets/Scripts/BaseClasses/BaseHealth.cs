using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    private float hp;
    [SerializeField] public float maxHp;
    [HideInInspector] public float invulnerabilityTimer = 0;

    public Action OnDie;

    public virtual float Hp
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, maxHp);
            if (hp == 0)
                OnDie?.Invoke();
        }
    }

    // Use this for initialization
    protected virtual void Awake()
    {
        Hp = maxHp;
    }
    protected virtual void Update()
    {
        invulnerabilityTimer = Mathf.Max(invulnerabilityTimer - Time.deltaTime, 0);
    }

    public virtual bool Damage(float damage, float invulnerabilityTime = 0)
    {
        if (IsInvulnerable) return false;
        Hp -= damage;
        invulnerabilityTimer = invulnerabilityTime;
        return true;
    }

    public bool IsInvulnerable => invulnerabilityTimer > 0;

    public bool IsDead => Hp == 0;
}