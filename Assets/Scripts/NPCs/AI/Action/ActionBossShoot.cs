using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Shoot", fileName = "ActionBossShoot")]
public class ActionBossShoot : AIAction
{
    private Vector2 aimDirection;

    public override void Act(StateController controller)
    {
        DeterminateAim(controller);
        ShootPlayer(controller);
    }

    private void ShootPlayer(StateController controller)
    {
        // Stop enemy
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);

        // Shoot
        if (controller.CharacterWeapon.CurrentWeapon != null)
        {
            // controller.CharacterWeapon.CurrentWeapon.WeaponAim.SetAim(aimDirection);
            // controller.CharacterWeapon.CurrentWeapon.UseWeapon();
            controller.CharacterWeapon.CurrentWeapon.RequestShot();
        }
    }

    private void DeterminateAim(StateController controller)
    {
        aimDirection = controller.Target.position - controller.transform.position;
    }
}
