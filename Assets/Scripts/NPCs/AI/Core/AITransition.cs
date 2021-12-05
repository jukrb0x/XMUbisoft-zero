using System;

[Serializable]
public class AITransition
{
    public AIDecision Decision;
    public AIState TrueState;
    public AIState FalseState;
}