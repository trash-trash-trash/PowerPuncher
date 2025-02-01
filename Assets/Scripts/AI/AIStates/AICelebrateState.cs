using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AICelebrateState : AIStateBase
{
    private float radius;
    private float speed;
    private float minDistance = 5f;
    private float maxDistance = 10f;
    
    public override void OnEnable()
    {
        base.OnEnable();
        radius = agent.radius;
        speed = agent.speed;
        agent.radius = 0.1f;
        agent.speed = 5f;
        MoveToRandomPointAway();
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        float random = Random.Range(3f, 5f);
        yield return new WaitForSeconds(random);
        aiBrain.ChangeState(AIStates.WalkToPlayer);
    }

    public void MoveToRandomPointAway()
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 targetPosition = agent.transform.position + randomDirection * randomDistance;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetPosition, out hit, randomDistance, NavMesh.AllAreas))
            agent.SetDestination(hit.position);
        else
            MoveToRandomPointAway();
    }

    void OnDisable()
    {
        agent.radius = radius;
        agent.speed = speed;
    }
}
