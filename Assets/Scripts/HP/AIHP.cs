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
           
            if(agent != null)
               agent.enabled = false;
            
            if(rb!=null)
                flingAndRotate.Explode(rb, pushDirection, pushForce);
        }
    }
}
