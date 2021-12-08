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
    private UnderAttacked _underAttacked;
    private GameObject weapons;
    private LevelManager levelManager;

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
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        if (!gameOverLabel) gameOverLabel = GameObject.Find("GameOverLabel").GetComponent<TextMeshProUGUI>();
        _underAttacked = GetComponent<UnderAttacked>();

        base.Awake();
        UIManager.Instance.SetUIPlayerStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }

    private void Start()
    {
        gameOverLabel.gameObject.SetActive(false);
        UpdateUI();
    }

    private void Update()
    {
        // TODO: for testing usage
        if (levelManager.isGodMode)
        {
            if (Input.GetKeyDown(KeyCode.L)) Damage(1);

            if (Input.GetKeyDown(KeyCode.K))
            {
                if (CurrentShield == maxShield && HealthPoint == MaxHealthPoint) return;
                if (HealthPoint != MaxHealthPoint)
                {
                    AddHealth(1);
                }
                else
                {
                    AddShield(1);
                }
            }
        }
    }

    private void UpdateUI()
    {
        UIManager.Instance.SetUIPlayerStates(HealthPoint, MaxHealthPoint, CurrentShield, maxShield);
    }

    // Take the amount of damage we pass in parameters
    public override void Damage(float damage)
    {
        AudioManager.Instance.PlayOneShot(AudioEnum.PlayerTakeDamage);
        _underAttacked.FlashScreen();

        if (!IsShieldBroken)
        {
            CurrentShield -= damage;
            UpdateUI();

            if (CurrentShield <= 0) IsShieldBroken = true;

            return;
        }


        base.Damage(damage);
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
            CurrentShield = maxShield;
        }

        IsShieldBroken = false;

        UpdateUI();
    }


    // Kills the game object
    private void Die()
    {
        if (character != null)
        {
            AudioManager.Instance.Play(AudioEnum.Failed);
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;

            weapon.enabled = false;
            Cursor.visible = true;
            weapons.SetActive(false);
            PlayerPrefs.DeleteAll();
        }

        if (destroyObject) DestroyObject();

        // Show Game Over
        gameOverLabel.gameObject.SetActive(true);
        levelManager.PauseGame();
    }
}