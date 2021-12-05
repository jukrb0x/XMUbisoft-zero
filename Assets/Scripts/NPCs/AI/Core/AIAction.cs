using UnityEngine;

public abstract class AIAction : ScriptableObject
{
    public abstract void Act(StateController controller);
}