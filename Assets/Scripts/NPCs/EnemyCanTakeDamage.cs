using UnityEngine;

public class EnemyCanTakeDamage : MonoBehaviour
{
    [SerializeField] private LayerMask DamageMask;
    private NPCHealth _npcHealth;

    private void Awake()
    {
        _npcHealth = GetComponent<NPCHealth>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (CheckLayer(other.gameObject.layer, DamageMask)) _npcHealth.Damage(1);
    }

    private bool CheckLayer(int layer, LayerMask objectMask)
    {
        return ((1 << layer) & objectMask) != 0;
    }
}