using UnityEngine;

public class ComponentBase : MonoBehaviour
{
    [Header("Sprite")] [SerializeField] private Sprite damagedSprite;

    [Header("Settings")] [SerializeField] private float initialHealth;

    [SerializeField] protected int damagePoint = 0;
    protected bool isDamageable;

    private BaseHealth health;
    private SpriteRenderer spriteRenderer;

    protected virtual void Start()
    {
        // TODO cannot get component 
        if (GetComponent<BaseHealth>())
        {
            health = GetComponent<BaseHealth>();

        }
        health.MaxHealthPoint = initialHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDamageable = damagePoint != 0;
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Projectile")) TakeDamage();
    // }

    protected void TakeDamage()
    {
        if (!isDamageable) return;
        health.Damage(damagePoint);

        if (health.HealthPoint > 0)
            spriteRenderer.sprite = damagedSprite;
        else
            Destroy(gameObject);
    }
}