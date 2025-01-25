using UnityEngine;

public class AIStateBase : MonoBehaviour
{
    public AIBrain aiBrain;

    public virtual void OnEnable()
    {
        aiBrain = GetComponentInParent<AIBrain>();
    }
}
