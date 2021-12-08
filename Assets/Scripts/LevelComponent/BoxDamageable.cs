using UnityEngine;

public class BoxDamageable : ComponentBase
{
    [SerializeField] private float initialHealth;
    [SerializeField] protected int damagePoint;

    [Header("Sprite")] [SerializeField] private Sprite damagedSprite;


    private BaseHealth health;


    protected override void Start()
    {
        base.Start();
        if (GetComponent<BaseHealth>()) health = GetComponent<BaseHealth>();

        health.MaxHealthPoint = initialHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            TakeDamage();
            
        }
    }

    private void TakeDamage()
    {
        health.Damage(damagePoint);
        if (health.HealthPoint > 0)
            spriteRenderer.sprite = damagedSprite;
        else
        {
            canReward = true;
            RewardPlayer();
            Destroy(gameObject);
        }
    }
}