using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpState : BossSpawnState
{
    public Transform jumpPoint;
    public Transform prevTransform;

    public List<Transform> bossJumpPoints = new List<Transform>();
    
    public override void OnEnable()
    {
        base.OnEnable();
        jumpPoint = Transform();
        StartCoroutine(WaitForSky());
    }
    
    Transform Transform()
    {
        Transform point;
        do
        {
            int index = Random.Range(0, bossJumpPoints.Count);
            point = bossJumpPoints[index];
        }
        while (point == prevTransform && bossJumpPoints.Count > 1);

        prevTransform = point;
        
        return point;
    }

    IEnumerator WaitForSky()
    {
        float randTime = Random.Range(3f, 5f);
        yield return new WaitForSeconds(randTime);

        Teleport();
    }

    void Teleport()
    {
        bossBrain.transform.position = jumpPoint.position;

        StartCoroutine(WaitForGround());
    }

    IEnumerator WaitForGround()
    {
        mTG.FlipMovingToGround(true);
        while (mTG.movingToGround)
        {
            yield return null;
        }
        bossBrain.ChangeState(BossStates.TurnToPlayer);
    }
}
