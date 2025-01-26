using System;
using UnityEngine;
using UnityEngine.AI;

public class AIWalkToPlayerState : AIStateBase
{
    public Vector3 playerPos;
    private Vector3 prevPos = Vector3.zero;
    private Transform playerTrans;
    
    public override void OnEnable()
    {
        base.OnEnable();
        playerTrans = PlayerSingleton.Instance.playerCapsule.transform;
        agent.enabled = true;
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
}
