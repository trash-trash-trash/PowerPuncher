using System.Collections;
using UnityEngine;

public class AIAttackState : AIStateBase
{
    public int sphereRadius = 3;
    
    public LayerMask layerMask;
    
    private Collider[] colliders = new Collider[10];
    
    public override void OnEnable()
    {
        base.OnEnable();
        

        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForFixedUpdate();
        
        PerformOverlapSphere();

        yield return new WaitForFixedUpdate();
        aiBrain.HP.ChangeHP(-999);
    }
    
    public void PerformOverlapSphere()
    {
        Vector3 sphereCenter = aiBrain.transform.position;

        int hitCount = Physics.OverlapSphereNonAlloc(sphereCenter, sphereRadius, colliders, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            HealthComponent health = colliders[i].GetComponentInParent<HealthComponent>();
            if (health != null)
            {
                health.ChangeHP(-10);
            }
        }
    }
}
