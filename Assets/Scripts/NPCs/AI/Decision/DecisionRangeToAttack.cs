using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Detect Range To Attack", fileName = "DecisionRangeToAttack")]
public class DecisionRangeToAttack : AIDecision
{
    public float minDistanceToAttack = 2f;
    
    public override bool Decide(StateController controller)
    {
        return PlayerInRangeToAttack(controller);
    }

    private bool PlayerInRangeToAttack(StateController controller)
    {
        if (controller.Target != null)
        {
            // Get Distance
            float distanceToAttack = (controller.Target.position - controller.transform.position).sqrMagnitude;
            
            // Compare and return if we are close to the target
            return distanceToAttack < Mathf.Pow(minDistanceToAttack, 2);
        }

        return false;
    }
}