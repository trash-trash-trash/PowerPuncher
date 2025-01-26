using System.Collections;
using UnityEngine;

public class AISpawnState : AIStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        Reset();
    }

    void Reset()
    {
        Rigidbody rb = GetComponentInParent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        
        CapsuleCollider collider = GetComponentInParent<CapsuleCollider>();
        collider.isTrigger = false;
        
        aiBrain.HP.Rez();

        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForFixedUpdate();
        aiBrain.ChangeState(AIStates.WalkToPlayer);
    }
}
