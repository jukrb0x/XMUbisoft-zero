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
            // TODO: buggy code
            healthPoint = (int) Mathf.Clamp(value, 0, maxHealthPoint);
            if (healthPoint == 0)
                OnDie?.Invoke();
        }
    }

    public float MaxHealthPoint
    {
        get => maxHealthPoint;

        set
        {
            maxHealthPoint = value;
            healthPoint = maxHealthPoint;
        }
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
        // TODO invoke onDie when damage
    }

    protected void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}