using UnityEngine;

public class AddShieldPotion : Collectables
{
    [SerializeField] private float ShieldPotionPoint;

    protected override void Collect()
    {
        base.Collect();
        character.health.AddShield(ShieldPotionPoint);
    }
}