using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private bool canDestroyItem = true;
    [SerializeField] public ParticleSystem collectablePS;

    protected Character character;
    protected GameObject objectCollided;
    protected SpriteRenderer spriteRenderer;
    protected Collider2D collider2D;
    protected CharacterController _controller;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        objectCollided = other.gameObject;
        if (IsCollectable())
        {
            if (other.gameObject.GetComponent<CharacterWeapon>().CurrentWeapon.CompareTag("Weapon_Initial") && other.gameObject.GetComponent<CharacterWeapon>().SecondaryWeapon!=null)
            {
                return;
            }
            Collect();
            PlayEffects();
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

    protected virtual void Collect()
    {
        
    }

    public void PlayEffects()
    {
        collectablePS.Play();
    }
}

