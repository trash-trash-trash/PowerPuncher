using System.Collections;
using UnityEngine;

public class AIDeathState : AIStateBase
{
    public Rigidbody rb;
    
    public FlingAndRotate flingAndRotate;

    public CapsuleCollider capCollider;
    
    public override void OnEnable()
    {
        base.OnEnable();
        aiBrain.miniMapComp.SetActive(false);
        aiBrain.pwrGge.IncreasePower(aiBrain.waveCount);
        aiBrain.HP.hpData.canTakeDamage = false;
        agent.enabled = false;
        StartCoroutine(WaitToDespawn());
    }

    private IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(3);
        capCollider = GetComponentInParent<CapsuleCollider>();
        capCollider.isTrigger = true;
        yield return new WaitForSeconds(5);
        aiBrain.gameObject.SetActive(false);
    }
}
