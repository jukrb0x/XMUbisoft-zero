using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Idle", fileName = "ActionIdle")]
public class ActionIdle : AIAction
{
    public override void Act(StateController controller)
    {
        controller.CharacterMovement.SetHorizontal(0);
        controller.CharacterMovement.SetVertical(0);
    }
}