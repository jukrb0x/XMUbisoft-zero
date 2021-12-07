using UnityEngine;

public class AddHealthPotion : Collectables
{
    [SerializeField] private float HealingPotionPoint;
    

    protected override void Collect()
    {
        base.Collect();
        character.health.AddHealth(HealingPotionPoint);
    }
}