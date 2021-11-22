using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BaseHealth : MonoBehaviour
{
    private float healthPoint;
    private float maxHealthPoint;
    public bool IsDead => HealthPoint <= 0;

    public Action OnDie;

    public float HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = (int) Mathf.Clamp(value, 0, maxHealthPoint);
            if (healthPoint == 0)
                OnDie?.Invoke();
        }
    }

    protected float MaxHealthPoint
    {
        get => maxHealthPoint;
        
        set => maxHealthPoint = value;
    }

    // Use this for initialization
    protected virtual void Awake()
    {
        healthPoint = maxHealthPoint;
    }

    public virtual void Damage(float damage)
    {
        // do not change this
        healthPoint -= damage;
    }

    protected void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}