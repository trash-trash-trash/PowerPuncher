using System.Collections;
using UnityEngine;

public class AIDeathState : AIStateBase
{
    public Rigidbody rb;
    
    public FlingAndRotate flingAndRotate;

    public CapsuleCollider collider;
    
    public override void OnEnable()
    {
        base.OnEnable();
        aiBrain.HP.hpData.canTakeDamage = false;
        agent.enabled = false;
        StartCoroutine(WaitToDespawn());
    }

    private IEnumerator WaitToDespawn()
    {
        yield return new WaitForSeconds(5);
        collider = GetComponentInParent<CapsuleCollider>();
        collider.isTrigger = true;
        
        yield return new WaitForSeconds(5);
        aiBrain.gameObject.SetActive(false);
    }
}
