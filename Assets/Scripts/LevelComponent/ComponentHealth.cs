using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")] [SerializeField] private float initialHealth = 10f;

    [SerializeField] private float maxHealth = 10f;

    [Header("Shield")] [SerializeField] private float initialShield = 5f;

    [SerializeField] private float maxShield = 5f;

    [Header("Settings")] [SerializeField] private bool destroyObject;

    private Character character;
    private Collider2D collider2D;
    private CharacterController controller;

    private bool isPlayer;
    private bool shieldBroken;
    private SpriteRenderer spriteRenderer;

    // Controls the current health of the object    
    public float CurrentHealth { get; set; }

    // Returns the current health of this character
    public float CurrentShield { get; set; }

    private void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharacterController>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        if (character != null) isPlayer = character.CharacterType == Character.CharacterTypes.Player;

        UIManager.Instance.SetUIStates(CurrentHealth, maxHealth, CurrentShield, maxShield);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) TakeDamage(1);
    }

    // Take the amount of damage we pass in parameters
    public void TakeDamage(int damage)
    {
        if (CurrentHealth <= 0) return;

        if (!shieldBroken && character != null)
        {
            CurrentShield -= damage;
            UIManager.Instance.SetUIStates(CurrentHealth, maxHealth, CurrentShield, maxShield);

            if (CurrentShield <= 0) shieldBroken = true;
            return;
        }

        CurrentHealth -= damage;
        UIManager.Instance.SetUIStates(CurrentHealth, maxHealth, CurrentShield, maxShield);

        if (CurrentHealth <= 0) Die();
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
        }

        if (destroyObject) DestroyObject();
    }

    // Revive this game object    
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            controller.enabled = true;
        }

        gameObject.SetActive(true);

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        shieldBroken = false;

        UIManager.Instance.SetUIStates(CurrentHealth, maxHealth, CurrentShield, maxShield);
    }

    // If destroyObject is selected, we destroy this game object
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}