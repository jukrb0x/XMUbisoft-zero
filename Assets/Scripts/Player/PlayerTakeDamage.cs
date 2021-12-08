using UnityEngine;

public class PlayerTakeDamage : MonoBehaviour
{
    [SerializeField] private float timeBtwDamage = 0.5f;

    private PlayerHealth _playerHealth;
    private float nextDamageTime;
    private bool PlayerCanTakeDamage = true;
    private Renderer myRender;
    
    private void Awake()
    {
        _playerHealth = GetComponent<PlayerHealth>();

    }
    
    protected virtual void Update()
    {
        CanTakeDamage();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
    
        if ((other.collider.CompareTag("Enemy") || other.collider.CompareTag("EnemyProjectile")) && PlayerCanTakeDamage)
        {
            _playerHealth.Damage(1);
            PlayerCanTakeDamage = false;
            nextDamageTime = Time.time + timeBtwDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("EnemyProjectile") && PlayerCanTakeDamage)
        {
            
            _playerHealth.Damage(1);
            PlayerCanTakeDamage = false;
            nextDamageTime = Time.time + timeBtwDamage;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((other.CompareTag("Damage_Spike") || other.CompareTag("Debuff_Poison")) && PlayerCanTakeDamage)
        {
            _playerHealth.Damage(1);
            PlayerCanTakeDamage = false;
            nextDamageTime = Time.time + timeBtwDamage;
        }
        if (other.CompareTag("Deth_Spike") && PlayerCanTakeDamage)
        {
            _playerHealth.Damage(15);
            PlayerCanTakeDamage = false;
            nextDamageTime = Time.time;
        }
    }
    
    private void CanTakeDamage()
    {
        if (Time.time > nextDamageTime) PlayerCanTakeDamage = true;
    }
}
