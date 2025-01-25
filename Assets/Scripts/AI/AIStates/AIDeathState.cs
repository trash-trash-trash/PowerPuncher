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
        rb = GetComponentInParent<Rigidbody>();
        
        flingAndRotate = SingletonTools.Instance.flingAndRotate;
        flingAndRotate.Explode(rb);
        
        SingletonTools.Instance.powerGauge.IncreasePower();

        StartCoroutine(WaitToDespawn());
    }

    private IEnumerator WaitToDespawn()
    {
        yield return new WaitForFixedUpdate();
        collider = GetComponentInParent<CapsuleCollider>();
        collider.isTrigger = true;
    }
}
