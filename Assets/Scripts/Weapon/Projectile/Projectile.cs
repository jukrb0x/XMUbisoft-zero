using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    // 返回此弹丸的方向
    public Vector2 Direction { get; set; }
    
    // 如果弹丸朝右，则返回 
    public bool FacingRight { get; set; }

    // 返回弹丸的速度 
    public float  Speed { get; set; }

    public Character ProjectileOwner { get; set; }
    
    // Internal
    private Rigidbody2D myRigidbody2D;
    private Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    
    private void Awake()
    {
        Speed = speed;
        FacingRight = true;

        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {       
        MoveProjectile();       
    }
    
    // 移动弹丸
    public void MoveProjectile()
    {
        movement = Direction * (Speed / 10f ) * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;
    }
   
    // 翻转弹丸
    public void FlipProjectile()
    {   
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
  
    // 设置方向和旋转以移动
    public void SetDirection(Vector2 newDirection, Quaternion rotation, bool isFacingRight = true)
    {
        Direction = newDirection;
        
        if (FacingRight != isFacingRight)
        {
            FlipProjectile();
        }

        transform.rotation = rotation;
    }
}