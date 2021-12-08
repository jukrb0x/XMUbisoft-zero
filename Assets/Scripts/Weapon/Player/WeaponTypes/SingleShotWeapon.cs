using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SingleShotWeapon : Weapon
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;

    // 控制弹丸出生的位置
    public Vector3 ProjectileSpawnPosition { get; set; }

    // 在此游戏对象中返回对池的引用
    public PlayerObjectPooler Pooler { get; set; }

    private Vector3 projectileSpawnValue;
    private Vector3 randomProjectileSpread;

    protected override void Awake()
    {
        base.Awake();
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;
        Pooler = GetComponent<PlayerObjectPooler>();
    }
    
    public override void RequestShot()
    {
        base.RequestShot();
        if (CanShoot && Input.GetMouseButtonDown(0))
        {
            muzzlePS.Play();
            EvaluateProjectileSpawnPosition();
            SpawnProjectile(ProjectileSpawnPosition);
        }
    }

    // 从池中生成弹丸，根据角色的方向设置新方向（武器所有者）
    private void SpawnProjectile(Vector2 spawnPosition)
    {
        // 从池中获取对象
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        // 获取弹丸的参考
        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        projectile.EnableProjectile();

        // 发散
        randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        Quaternion spread = Quaternion.Euler(randomProjectileSpread);

        // 设置方向和旋转
        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? spread * transform.right : spread * transform.right * -1;
        projectile.SetDirection(newDirection, transform.rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);

        AudioManager.Instance.Play(AudioEnum.SingleGunAndAK47Shoot);
        CanShoot = false;  
        nextShotTime = Time.time + timeBtwShots;
    }

    // 计算弹丸发射位置
    private void EvaluateProjectileSpawnPosition()
    {
        if (WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            // 朝右
            ProjectileSpawnPosition = transform.position + transform.rotation * projectileSpawnPosition;
        }
        else
        {
            // 朝左
            ProjectileSpawnPosition = transform.position - transform.rotation * projectileSpawnValue;
        }       
    }

    // private void OnDrawGizmosSelected()
    // {
    //     EvaluateProjectileSpawnPosition();
    //
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    // }
}
