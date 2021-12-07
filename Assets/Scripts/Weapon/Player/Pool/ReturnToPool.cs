using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private LayerMask WallMask;

    [SerializeField] private LayerMask EnemyMask;
    [SerializeField] private float lifeTime = 2f;

    private Projectile projectile;

    private void Start()
    {
        projectile = GetComponent<Projectile>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Return), lifeTime);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, WallMask))
        {
            AudioManager.Instance.PlayOneShot(AudioEnum.ProjectileHitWall);
            Return();
        }

        if (CheckLayer(other.gameObject.layer, EnemyMask)) Return();
    }

    // Returns this object to the pool
    private void Return()
    {
        if (projectile != null) projectile.ResetProjectile();
        gameObject.SetActive(false);
    }

    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }
}