using System;
using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    private float healthPoint;
    private float maxHealthPoint;

    public Action OnDie;
    public bool IsDead => HealthPoint <= 0;

    public float HealthPoint
    {
        get => healthPoint;
        set
        {
            healthPoint = (int) Mathf.Clamp(value, 0, maxHealthPoint);
            if (healthPoint == 0)
            {
                OnDie?.Invoke();
            }
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

    protected virtual void Awake()
    {
        healthPoint = maxHealthPoint;
    }

    public virtual void Damage(float damage)
    {
        healthPoint -= damage;
        // TODO invoke onDie when damage
    }

    protected void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}