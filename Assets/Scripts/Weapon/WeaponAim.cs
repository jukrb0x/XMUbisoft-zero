using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;

    // Returns the current absolute angle
    public float CurrentAimAngleAbsolute { get; set; }
    
    // Returns the current angle
    public float CurrentAimAngle { get; set; }

    private Camera mainCamera;
	private GameObject reticle;
	private Weapon weapon;

    private Vector3 direction;
    private Vector3 mousePosition;
	private Vector3 reticlePosition;
    private Vector3 currentAim = Vector3.zero;
    private Vector3 currentAimAbsolute = Vector3.zero;
    private Quaternion initialRotation;
    private Quaternion lookRotation;

    private void Start()
    {
        Cursor.visible = false; 
        weapon = GetComponent<Weapon>();
        initialRotation = transform.rotation;  
     
        mainCamera = Camera.main;
        reticle = Instantiate(reticlePrefab);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Cursor.visible = true;
        }
        GetMousePosition();                
        MoveReticle();
        RotateWeapon();
    }

    // 获取精确的鼠标位置以达到目标
    private void GetMousePosition()
    {
        // 获取鼠标位置
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f;  // We set this value to ensure the camera always stays infront to view everything in game

        // 获得世界空间位置
        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
        {
            currentAim = direction - transform.position;
        }
        else
        {
            currentAim = transform.position - direction;
        }
	}

    public void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero)
        {
            // 获得角度
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

            // 制造旋转？
            if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            else
            {
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            }
            
            // 应用角度
            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;
        }
        else
        {
            CurrentAimAngle = 0f;  // 如果鼠标在开始时根本无法移动
            transform.rotation = initialRotation;
        }
    }

    // 将瞄准镜移向鼠标位置
    private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity; //设置正常旋转
        reticle.transform.position = reticlePosition;
    }
}