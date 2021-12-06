using UnityEngine;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;

    private Vector3 projectileSpawnValue;
    private Vector3 randomProjectileSpread;

    // 控制弹丸出生的位置
    public Vector3 ProjectileSpawnPosition { get; set; }

    // 在此游戏对象中返回对池的引用
    public ObjectPooler Pooler { get; set; }

    protected override void Awake()
    {
        base.Awake();
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;
        Pooler = GetComponent<ObjectPooler>();
    }

    private void OnDrawGizmosSelected()
    {
        EvaluateProjectileSpawnPosition();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    }

    public override void RequestShot()
    {
        base.RequestShot();
        if (CanShoot && Input.GetMouseButton(0))
        {
            EvaluateProjectileSpawnPosition();
            SpawnProjectile(ProjectileSpawnPosition);
        }
    }

    // 从池中生成弹丸，根据角色的方向设置新方向（武器所有者）
    private void SpawnProjectile(Vector2 spawnPosition)
    {
        // 从池中获取对象
        var projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        // 获取弹丸的参考
        var projectile = projectilePooled.GetComponent<Projectile>();

        // 发散
        randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        var spread = Quaternion.Euler(randomProjectileSpread);

        // 设置方向和旋转
        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? spread * transform.right : spread * transform.right * -1;
        projectile.SetDirection(newDirection, transform.rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);

        AudioManager.Instance.Play(AudioEnum.ShotGunShoot);
        CanShoot = false;  
        nextShotTime = Time.time + timeBtwShots;
    }

    // 计算弹丸发射位置
    private void EvaluateProjectileSpawnPosition()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            // 朝右
            ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
        else
            // 朝左
            ProjectileSpawnPosition = transform.position - transform.rotation * projectileSpawnValue;
    }
}