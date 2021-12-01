using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGunWeapon : Weapon
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;
    [SerializeField] private int maxProjectileOneShot = 3;

    private int projectileOneShot = 0;
    // 控制弹丸出生的位置
    public Vector3 ProjectileSpawnPosition { get; set; }

    // 在此游戏对象中返回对池的引用
    public ObjectPooler Pooler { get; set; }

    private Vector3 projectileSpawnValue;
    private Vector3 randomProjectileSpread;
    private Vector3 rotationGun;

    protected override void Awake()
    {
        base.Awake();
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y;
        Pooler = GetComponent<ObjectPooler>();
    }
    
    public override void RequestShot()
    {
        base.RequestShot();
        if (CanShoot)
        {
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
        Quaternion rotation;
        // 发散
        if (projectileOneShot == 1)
        {
            rotationGun = transform.position;
            rotationGun.z += 10;
            rotation = Quaternion.Euler(rotationGun);
        }
        else if (projectileOneShot == 2)
        {
            rotationGun = transform.position;
            rotationGun.z -= 10;
            rotation = Quaternion.Euler(rotationGun);
        }
        else
        {
            rotationGun = transform.position;
            rotation = Quaternion.Euler(rotationGun);
            
        }
        
        // randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        //randomProjectileSpread.z = projectileSpread.z;
        // Quaternion spread = Quaternion.Euler(randomProjectileSpread);
        

        // 设置方向和旋转
        Vector2 newDirection = WeaponOwner.GetComponent<CharacterFlip>().FacingRight ? rotation * transform.right : rotation * transform.right * -1;
        projectile.SetDirection(newDirection, rotation, WeaponOwner.GetComponent<CharacterFlip>().FacingRight);

        projectileOneShot++;
        if (projectileOneShot >= maxProjectileOneShot)
        {
            projectileOneShot = 0;
            CanShoot = false;  
            nextShotTime = Time.time + timeBtwShots;
        }

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

    private void OnDrawGizmosSelected()
    {
        EvaluateProjectileSpawnPosition();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    }
}
