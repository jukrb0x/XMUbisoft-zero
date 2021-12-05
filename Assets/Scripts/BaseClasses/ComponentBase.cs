using UnityEngine;

// Component base defines the rewardable level components
public class ComponentBase : MonoBehaviour
{
    [SerializeField] protected GameObject[] rewards;

    [Header("Reward Position")] [SerializeField]
    private float xRandomPosition = 2f;

    [SerializeField] private float yRandomPosition = 2f;

    [Header("Settings")] protected bool canReward = false;

    private bool rewardDelivered;
    private Vector3 rewardRandomPosition;
    protected SpriteRenderer spriteRenderer;


    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void RewardPlayer()
    {
        if (canReward && !rewardDelivered)
        {
            rewardRandomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
            rewardRandomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);
            Instantiate(SelectReward(), transform.position + rewardRandomPosition, Quaternion.identity);
            rewardDelivered = true;
        }
    }

    private GameObject SelectReward()
    {
        var randomRewardIndex = Random.Range(0, rewards.Length);
        return rewards[randomRewardIndex];
    }
}