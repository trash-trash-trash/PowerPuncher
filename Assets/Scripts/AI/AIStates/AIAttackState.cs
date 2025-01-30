using System.Collections;
using UnityEngine;

public class AIAttackState : AIStateBase
{
    private float sphereRadius = 1;
    
    public LayerMask layerMask;
    
    private Collider[] colliders = new Collider[10];
    
    public override void OnEnable()
    {
        base.OnEnable();

        sphereRadius = 2f * aiBrain.transform.localScale.x;
        
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForFixedUpdate();
        
        PerformOverlapSphere();
        
        float randSecond = Random.Range(.1f, 2.5f);
        
        yield return new WaitForSeconds(randSecond);
        
        aiBrain.ChangeState(AIStates.WalkToPlayer);
    }
    
    public void PerformOverlapSphere()
    {
        Vector3 sphereCenter = aiBrain.transform.position;

        int hitCount = Physics.OverlapSphereNonAlloc(sphereCenter, sphereRadius, colliders, layerMask);

        for (int i = 0; i < hitCount; i++)
        {
            PlayerHP health = colliders[i].GetComponentInParent<PlayerHP>();
            if (health != null)
            {
                health.ChangeHP(-10 * aiBrain.waveCount);
            }
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
