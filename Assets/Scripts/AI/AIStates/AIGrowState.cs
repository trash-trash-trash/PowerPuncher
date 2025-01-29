using System.Collections;
using UnityEngine;

public class AIGrowState : AIStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        float increaseAmount = aiBrain.transform.localScale.x + 0.1f * aiBrain.waveCount;

        float maxSize = 3.5f;
        if (increaseAmount > maxSize)
            increaseAmount = maxSize;
        
        float random = Random.Range(1f, increaseAmount);
         
        aiBrain.transform.localScale *= random;
         
         if (aiBrain.transform.localScale.x > maxSize) 
         {
             aiBrain.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
         }
         
        yield return new WaitForFixedUpdate();
        
        aiBrain.ChangeState(AIStates.Spawn);
    }
}
