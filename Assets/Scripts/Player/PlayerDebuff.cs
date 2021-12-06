using UnityEngine;

public class PlayerDebuff : MonoBehaviour
{
    [SerializeField] private float SlowSpeed = 2f;
    private CharacterMovement _characterMovement;

    private void Awake()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Slow")) _characterMovement.ResetSpeed();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Debuff_Slow")) _characterMovement.MoveSpeed = SlowSpeed;
    }
}