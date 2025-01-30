using UnityEngine;
using UnityEngine.AI;

public class AIStateBase : MonoBehaviour
{
    public AIBrain aiBrain;
    
    public NavMeshAgent agent;
    
    public virtual void OnEnable()
    {
        aiBrain = GetComponentInParent<AIBrain>();
        agent = GetComponentInParent<NavMeshAgent>();
    }
}
