using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [SerializeField] private GameObject reticlePrefab;
    private Vector3 currentAim = Vector3.zero;
    private Vector3 currentAimAbsolute = Vector3.zero;

    private Vector3 direction;
    private Quaternion initialRotation;
    private Quaternion lookRotation;

    private Camera mainCamera;
    private Vector3 mousePosition;
    private GameObject reticle;
    private Vector3 reticlePosition;
    private Weapon weapon;

    // Returns the current absolute angle
    public float CurrentAimAngleAbsolute { get; set; }

    // Returns the current angle
    public float CurrentAimAngle { get; set; }

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
        GetMousePosition();
        MoveReticle();
        RotateWeapon();
    }

    // Get the exact mouse position in order to aim
    private void GetMousePosition()
    {
        // Get Mouse Position
        mousePosition = Input.mousePosition;
        mousePosition.z = 5f; // We set this value to ensure the camera always stays infront to view everything in game

        // Get World space position
        direction = mainCamera.ScreenToWorldPoint(mousePosition);
        direction.z = transform.position.z;
        reticlePosition = direction;

        currentAimAbsolute = direction - transform.position;
        if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
            currentAim = direction - transform.position;
        else
            currentAim = transform.position - direction;
    }

    public void SetAim(Vector2 newAim)
    {
        currentAim = newAim;
    }
    
    public void RotateWeapon()
    {
        if (currentAim != Vector3.zero && direction != Vector3.zero)
        {
            // Get Angle
            CurrentAimAngle = Mathf.Atan2(currentAim.y, currentAim.x) * Mathf.Rad2Deg;
            CurrentAimAngleAbsolute = Mathf.Atan2(currentAimAbsolute.y, currentAimAbsolute.x) * Mathf.Rad2Deg;

            // Clamp our rotation
            if (weapon.WeaponOwner.GetComponent<CharacterFlip>().FacingRight)
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);
            else
                CurrentAimAngle = Mathf.Clamp(CurrentAimAngle, -180, 180);

            // Apply the angle
            lookRotation = Quaternion.Euler(CurrentAimAngle * Vector3.forward);
            transform.rotation = lookRotation;
        }
        else
        {
            CurrentAimAngle = 0f; // If the mouse is not moving at all at the beginning
            transform.rotation = initialRotation;
        }
    }

    // Moves our reticle towards our Mouse Position
    private void MoveReticle()
    {
        reticle.transform.rotation = Quaternion.identity; //set the normal rotation
        reticle.transform.position = reticlePosition;
    }
    public void DestroyReticle()
    {
        Destroy(reticle.gameObject);
    }

}