using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration;
    private Collider2D collider2D;
    private Vector2 movement;

    // Internal
    private Rigidbody2D myRigidbody2D;
    private SpriteRenderer spriteRenderer;

    // 返回此弹丸的方向
    public Vector2 Direction { get; set; }

    // 如果弹丸朝右，则返回 
    public bool FacingRight { get; set; }

    // 返回弹丸的速度 
    public float Speed { get; set; }

    public Character ProjectileOwner { get; set; }
    
    private bool canMove;
    
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
        if (canMove)
        {
            MoveProjectile();
        }
     
    }

    // 移动弹丸
    public void MoveProjectile()
    {
        movement = Direction * (Speed / 10f) * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;
    }

    // 翻转弹丸
    public void FlipProjectile()
    {
        if (spriteRenderer != null) spriteRenderer.flipX = !spriteRenderer.flipX;
    }

    // 设置方向和旋转以移动
    public void SetDirection(Vector2 newDirection, Quaternion rotation, bool isFacingRight = true)
    {
        Direction = newDirection;

        if (FacingRight != isFacingRight) FlipProjectile();

        transform.rotation = rotation;
    }

    public void ResetProjectile()
    {
        spriteRenderer.flipX = false;
    }
    
    public void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;  // If we don’t disable the spriteRenderer, the bullet will fall down before disappear
        collider2D.enabled = false;
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }

}