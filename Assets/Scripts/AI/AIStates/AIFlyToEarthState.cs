using System.Collections;
using UnityEngine;

public class AIFlyToEarthState : AIStateBase
{
    public MoveToGround moveToGround;

    public GameObject landingAnimation;
    
    public override void OnEnable()
    {
        base.OnEnable();
        moveToGround = GetComponentInParent<MoveToGround>();
        moveToGround.FlipMovingToGround(true);
        StartCoroutine(WaitForFly());
    }

    IEnumerator WaitForFly()
    {
        while (moveToGround.movingToGround)
        {
            yield return new WaitForFixedUpdate();
        }

        Instantiate(landingAnimation);
        landingAnimation.transform.position = new Vector3(aiBrain.transform.position.x, moveToGround.groundY+.1f, aiBrain.transform.position.z);
        
        aiBrain.ChangeState(AIStates.WalkToPlayer);
    }
}
