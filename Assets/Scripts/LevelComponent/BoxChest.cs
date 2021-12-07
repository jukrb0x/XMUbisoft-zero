using UnityEngine;

public class BoxChest : ComponentBase
{
    private readonly int boxTreasureOpenedParameter = Animator.StringToHash("Rewarded");
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && canReward) RewardPlayer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canReward = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) canReward = false;
    }

    protected override void RewardPlayer()
    {
        animator.SetTrigger(boxTreasureOpenedParameter);
        AudioManager.Instance.PlayOneShot(AudioEnum.ChestOpen);
        base.RewardPlayer();
    }
}