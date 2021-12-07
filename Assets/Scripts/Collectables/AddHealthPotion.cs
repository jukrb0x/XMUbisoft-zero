using UnityEngine;

public class AddHealthPotion : Collectables
{
    [SerializeField] private float HealingPotionPoint;
    

    protected override void Collect()
    {
        base.Collect();
        AudioManager.Instance.Play(AudioEnum.UsePotion);
        character.health.AddHealth(HealingPotionPoint);
    }
}