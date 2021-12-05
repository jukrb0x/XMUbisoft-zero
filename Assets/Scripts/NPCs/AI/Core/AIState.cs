using UnityEngine;

[CreateAssetMenu(menuName = "AI/State")]
public class AIState : ScriptableObject
{
    public AIAction[] Actions;
    public AITransition[] Transitions;

    public void EvaluateState(StateController controller)
    {
        DoActions(controller);
        EvaluateTransitions(controller);
    }

    public void DoActions(StateController controller)
    {
        foreach (var action in Actions) action.Act(controller);
    }

    public void EvaluateTransitions(StateController controller)
    {
        if (Transitions != null || Transitions.Length > 1)
            for (var i = 0; i < Transitions.Length; i++)
            {
                var decisionResult = Transitions[i].Decision.Decide(controller);
                if (decisionResult)
                    controller.TransitionToState(Transitions[i].TrueState);
                else
                    controller.TransitionToState(Transitions[i].FalseState);
            }
    }
}