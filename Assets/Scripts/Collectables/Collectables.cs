using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [Header("Settings")] [SerializeField] private bool canDestroyItem = true;

    protected Character character;
    protected GameObject objectCollided;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2D;
    protected CharacterController _controller;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        // collectablePS = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canDestroyItem = true;
        objectCollided = other.gameObject;
        if (IsCollectable())
        {
            if (other.gameObject.GetComponent<CharacterWeapon>().CurrentWeapon.CompareTag("Weapon_Initial") &&
                other.gameObject.GetComponent<CharacterWeapon>().SecondaryWeapon != null)
            {
                canDestroyItem = false;
                return;
            }

            Collect();

            if (canDestroyItem)
            {
                Destroy(gameObject);
            }
            else
            {
                spriteRenderer.enabled = false;
                collider2D.enabled = false;
            }
        }
    }

    protected virtual bool IsCollectable()
    {
        character = objectCollided.GetComponent<Character>();
        if (character == null)
        {
            return false;
        }

        return character.CharacterType == Character.CharacterTypes.Player;
    }

    /// <summary>
    ///  Character collects items on the map
    /// </summary>
    /// <returns>return bool indicating collect succeeded or not</returns>
    protected virtual bool Collect()
    {
        return true;
    }
}