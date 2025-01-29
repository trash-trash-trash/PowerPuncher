using UnityEngine;
using UnityEngine.AI;

public class AIHP : HealthComponent
{
    public NavMeshAgent agent;
    public Vector2 pushDirection;
    public float pushForce;
    public Rigidbody rb;
    public FlingAndRotate flingAndRotate;

    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);

        if (!hpData.isAlive)
        {
            hpData.canTakeDamage = false;
            agent.enabled = false;
             
            flingAndRotate = SingletonTools.Instance.flingAndRotate;
            flingAndRotate.Explode(rb, pushDirection, pushForce);
        }
    }
}
