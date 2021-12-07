using TMPro;
using UnityEngine;

public class PlayerHealth : BaseHealth
{
    [Header("Health")] [SerializeField] private float initialHealth = 10f;
    [Header("Shield")] [SerializeField] private float initialShield = 5f;
    [SerializeField] private float maxShield = 5f;
    [SerializeField] private TextMeshProUGUI gameOverLabel;

    [Header("Settings")] [SerializeField] private bool destroyObject;

    public float CurrentShield;

    private Character character;
    private Collider2D collider2D;
    private CharacterController controller;

    private bool IsShieldBroken;
    private SpriteRenderer spriteRenderer;
    private CharacterWeapon weapon;
    private GameObject weapons;

    protected override void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        weapon = GetComponent<CharacterWeapon>();
        weapons = GameObject.Find("WeaponHolder");
        // TODO: Fix Aim Disappear Problem
        // weapons = GameObject.FindWithTag("Weapon1");
        MaxHealthPoint = initialHealth;
        CurrentShield = initialShield;
        if (!gameOverLabel) gameOverLabel = GameObject.Find("GameOverLabel").GetComponent<TextMeshProUGUI>();

        base.Awake();
        UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }

    private void Start()
    {
        gameOverLabel.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.L)) Damage(1);
    }

    private void UpdateUI()
    {
        UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }
    
    // Take the amount of damage we pass in parameters
    public override void Damage(float damage)
    {
        if (!IsShieldBroken)
        {
            CurrentShield -= damage;
            // UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
            UpdateUI();

            if (CurrentShield <= 0) IsShieldBroken = true;

            return;
        }

        base.Damage(damage);
        // UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
        UpdateUI();
        if (IsDead) Die();
    }

    // Improve Health Point of character
    public void AddHealth(float hp)
    {
        if (MaxHealthPoint - HealthPoint >= hp)
        {
            HealthPoint += hp;
        }
        else
        {
            HealthPoint = MaxHealthPoint;
        }
        UpdateUI();
    }

    // Reinforce Shield Point of character
    public void AddShield(float shield)
    {
        if (maxShield - CurrentShield >= shield)
        {
            CurrentShield += shield;
        }
        else
        {
            CurrentShield = shield;
        }
        UpdateUI();
    }


    // Kills the game object
    private void Die()
    {
        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;

            weapon.enabled = false;
            Cursor.visible = true;
            weapons.SetActive(false);
        }

        if (destroyObject) DestroyObject();

        // show Game Over label
        gameOverLabel.gameObject.SetActive(true);
    }

    // Revive this game object    
    // FIXME: awake --> start
    // public void Revive()
    // {
    //     if (character != null)
    //     {
    //         collider2D.enabled = true;
    //         spriteRenderer.enabled = true;
    //
    //         character.enabled = true;
    //         controller.enabled = true;
    //
    //         weapon.enabled = true;
    //         Cursor.visible = false;
    //         weapons.SetActive(true);
    //     }
    //
    //     gameObject.SetActive(true);
    //
    //     HealthPoint = initialHealth;
    //     CurrentShield = initialShield;
    //
    //     IsShieldBroken = false;
    //
    //     UIManager.Instance.SetUIStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    // }
}