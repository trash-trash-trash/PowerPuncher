using System.Collections;
using UnityEngine;

public class BossLookAtPlayerState : BossStateBase
{
    public float rotationSpeed = 50f;
    
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(WaitRandomTimeToAttack());
    }

    IEnumerator WaitRandomTimeToAttack()
    {
        float rand = Random.Range(4f, 6f);
        yield return new WaitForSeconds(rand);
        bossBrain.ChangeState(BossStates.Attack);
    }
    
    void Update()
    {
        Vector3 direction = bossBrain.playerTransform.position - bossBrain.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        bossBrain.transform.rotation = Quaternion.Slerp(bossBrain.transform.rotation, new Quaternion (0,targetRotation.y,0,targetRotation.w), Time.deltaTime * rotationSpeed);
    }
}
