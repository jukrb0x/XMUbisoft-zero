using UnityEngine;

public class AddShieldPotion : Collectables
{
    [SerializeField] private float ShieldPotionPoint;

    protected override void Collect()
    {
        base.Collect();
        AudioManager.Instance.Play(AudioEnum.UsePotion);
        character.health.AddShield(ShieldPotionPoint);
    }
}