using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Reward Position")] [SerializeField]
    private float xRandomPosition = 2f;

    [SerializeField] private float yRandomPosition = 2f;

    [SerializeField] private GameObject[] rewards;
    private readonly int chestOpenedParameter = Animator.StringToHash("Rewarded");

    private Animator animator;

    private bool canReward;
    private bool rewardDelivered;
    private Vector3 rewardRandomPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            if (canReward)
                RewardPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canReward = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canReward = false;
    }

    private void RewardPlayer()
    {
        if (canReward && !rewardDelivered)
        {
            animator.SetTrigger(chestOpenedParameter);
            rewardRandomPosition.x = Random.Range(-xRandomPosition, xRandomPosition);
            rewardRandomPosition.y = Random.Range(-yRandomPosition, yRandomPosition);
            Instantiate(SelectReward(), transform.position + rewardRandomPosition, Quaternion.identity);

            rewardDelivered = true;
        }
    }

    private GameObject SelectReward()
    {
        var randomRewardIndex = Random.Range(0, rewards.Length);
        for (var i = 0; i < rewards.Length; i++) return rewards[randomRewardIndex];

        return null;
    }
}