using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Follow", fileName = "ActionFollow")]
public class ActionFollow : AIAction
{
    public float minDistanceToFollow = 2f;

    public override void Act(StateController controller)
    {
        FollowTarget(controller);
    }

    private void FollowTarget(StateController controller)
    {
        if (controller.Target == null) return;

        // Follow Horizontal
        if (controller.transform.position.x < controller.Target.position.x)
            controller.CharacterMovement.SetHorizontal(1);
        else
            controller.CharacterMovement.SetHorizontal(-1);

        // Follow Vertical
        if (controller.transform.position.y < controller.Target.position.y)
            controller.CharacterMovement.SetVertical(1);
        else
            controller.CharacterMovement.SetVertical(-1);

        // Stop if min distance reached (Horizontal)
        if (Mathf.Abs(controller.transform.position.x - controller.Target.position.x) < minDistanceToFollow)
            controller.CharacterMovement.SetHorizontal(0);

        // Stop if min distance reached (Vertical)
        if (Mathf.Abs(controller.transform.position.y - controller.Target.position.y) < minDistanceToFollow)
            controller.CharacterMovement.SetVertical(0);
    }
}