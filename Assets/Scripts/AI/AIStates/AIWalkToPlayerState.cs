using System;
using UnityEngine;
using UnityEngine.AI;

public class AIWalkToPlayerState : AIStateBase
{
    public float currentDist;
    public float minDist;
    
    public Vector3 playerPos;
    private Vector3 prevPos = Vector3.zero;
    private Transform playerTrans;

    public float maxSpeed=10f;
    
    private Rigidbody rb;
    
    public override void OnEnable()
    {
        base.OnEnable();
        playerTrans = PlayerSingleton.Instance.playerCapsule.transform;
        agent.enabled = true;
        rb = GetComponentInParent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (agent.isOnNavMesh)
        {
            playerPos = playerTrans.position;
            if (playerPos != prevPos)
            {
                agent.SetDestination(playerPos);
                prevPos = playerPos;
            }
        }
    }

    public void Update()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
        
        currentDist = Vector3.Distance(aiBrain.transform.position, playerTrans.position);
        if(currentDist < minDist)
            aiBrain.ChangeState(AIStates.Attack);
    }

    void OnDisable()
    {
        currentDist = 99999;
    }
}
