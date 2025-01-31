using System.Collections;
using UnityEngine;

public class AIAttackState : AIStateBase
{
    private float sphereRadius = 1;
    
    public LayerMask layerMask;

    public bool hitPlayer;
    
    private Collider[] colliders = new Collider[10];
    
    public override void OnEnable()
    {
        base.OnEnable();
        hitPlayer = false;
        sphereRadius = 5f * aiBrain.transform.localScale.x;
        
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForFixedUpdate();

        int charges = 0;
        while (charges < 15 || !hitPlayer)
        {
            PerformOverlapSphere();
            charges++;
            yield return new WaitForFixedUpdate();
        }
        
        aiBrain.ChangeState(AIStates.Celebrate);
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
                if (!hitPlayer)
                {
                    health.ChangeHP(-10 * aiBrain.waveCount);

                    Rigidbody rb = colliders[i].GetComponent<Rigidbody>();
                    Vector3 pushDirection = colliders[i].transform.position - aiBrain.transform.position;
                    pushDirection.Normalize();
                    Vector3 finalDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
                    rb.AddForce(finalDirection * 50, ForceMode.Impulse);
                    hitPlayer = true;
                }
            } 
        }
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }
}
