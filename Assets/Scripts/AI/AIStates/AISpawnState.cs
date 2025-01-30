using System.Collections;
using UnityEngine;

public class AISpawnState : AIStateBase
{
    private Rigidbody rb;
    public override void OnEnable()
    {
        base.OnEnable();
        aiBrain.miniMapComp.SetActive(true);
        rb = GetComponentInParent<Rigidbody>();
        StartCoroutine(HackWait());
    }
    
    IEnumerator HackWait()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        yield return new WaitForFixedUpdate();   
        Reset();
    }
    
    void Reset()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
       
        aiBrain.HP.Rez(); 
        CapsuleCollider collider = GetComponentInParent<CapsuleCollider>(); 
        collider.isTrigger = false;
        aiBrain.ChangeState(AIStates.FlyToEarth);
    }
}
