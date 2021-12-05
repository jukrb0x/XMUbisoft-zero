using System.Data.Common;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class Box : ComponentBase
{
    [Header("Reward Position")] [SerializeField]
    private float xRandomPosition = 2f;

    [SerializeField] private float yRandomPosition = 2f;

    [Header("Settings")] [SerializeField] private GameObject[] rewards;


    [SerializeField] private bool canReward = false;
    private int chestOpenedParameter = Animator.StringToHash("Rewarded");

    private Animator animator;

    private bool rewardDelivered;
    private Vector3 rewardRandomPosition;


    protected override void Start()
    {
        base.Start();
        if (!isDamageable)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!isDamageable)
            {
                RewardPlayer();
            }
        }
    }

    private void OnDestroy()
    {
        if (canReward) RewardPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile")) TakeDamage();
        if (!isDamageable)
        {
            if (other.CompareTag("Player")) canReward = true;

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isDamageable)
        {
            if (other.CompareTag("Player")) canReward = false;
        }
        
    }

    private void RewardPlayer()
    {
        if (canReward && !rewardDelivered)
        {
            if (!isDamageable) animator.SetTrigger(chestOpenedParameter);
            rewardRandomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
            rewardRandomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);
            Instantiate(SelectReward(), transform.position + rewardRandomPosition, Quaternion.identity);
            rewardDelivered = true;
        }
    }

    private GameObject SelectReward()
    {
        int randomRewardIndex = Random.Range(0, rewards.Length);
        return rewards[randomRewardIndex];
    }
}