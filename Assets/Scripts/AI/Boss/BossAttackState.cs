using System.Collections;
using UnityEngine;

public class BossAttackState : BossStateBase
{
    public ProjectileShooter projectile;

    public AISpawner spawner;
    
    public override void OnEnable()
    {
        base.OnEnable();
        
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < 50)
            StartCoroutine(HackAttackWait(projectile.numShots * projectile.fireRate));
        else
            StartCoroutine(HackSpawnWait());
    }

    IEnumerator HackAttackWait(float waitTime)
    {
        projectile.ShootMachineGun();
        yield return new WaitForSeconds(waitTime);
        PushPlayerBack();
    }

    IEnumerator HackSpawnWait()
    {
        int counter = Random.Range(1, 5);
        int count = 0;
        while (count < counter)
        {
            spawner.SpawnInCircle();
            count++;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3f);
        PushPlayerBack();
    }

    public void PushPlayerBack()
    {
        Rigidbody rb = bossBrain.playerTransform.GetComponent<Rigidbody>();
        Vector3 pushDirection =  bossBrain.transform.position -bossBrain.playerTransform.position;
        pushDirection.Normalize();
        Vector3 finalDirection = new Vector3(pushDirection.x, 1, pushDirection.z);
        rb.AddForce(finalDirection * 50, ForceMode.Impulse);
        
        bossBrain.ChangeState(BossStates.TurnToPlayer);
    }
}
