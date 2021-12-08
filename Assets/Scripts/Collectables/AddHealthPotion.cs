using UnityEngine;

public class AddHealthPotion : Collectables
{
    [SerializeField] private float HealingPotionPoint;
    

    protected override bool Collect()
    {
        base.Collect();
        AudioManager.Instance.Play(AudioEnum.UsePotion);
        character.health.AddHealth(HealingPotionPoint);
        return true;
    }
}