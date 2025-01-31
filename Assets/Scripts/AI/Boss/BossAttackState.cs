using System.Collections;
using UnityEngine;

public class BossAttackState : BossStateBase
{
    public ProjectileShooter projectile;

    public AISpawner spawner;
    
    public override void OnEnable()
    {
        base.OnEnable();
        PushPlayerBack();
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand < 75)
            StartCoroutine(HackAttackWait(projectile.numShots * projectile.fireRate));
        else
            StartCoroutine(HackSpawnWait());
    }

    IEnumerator HackAttackWait(float waitTime)
    {
        projectile.ShootMachineGun();
        yield return new WaitForSeconds(waitTime);
        bossBrain.ChangeState(BossStates.TurnToPlayer);
    }

    IEnumerator HackSpawnWait()
    {
        int counter = Random.Range(25,50);
        int count = 0;
        while (count < counter)
        {
            spawner.SpawnInCircle();
            count++;
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(3f);
        bossBrain.ChangeState(BossStates.TurnToPlayer);
    }

    public void PushPlayerBack()
    {
        Rigidbody rb = bossBrain.playerTransform.GetComponent<Rigidbody>();
        Vector3 pushDirection =  Vector3.zero -bossBrain.playerTransform.position;
        pushDirection.Normalize();
        Vector3 finalDirection = new Vector3(pushDirection.x, 0, pushDirection.z);
        rb.AddForce(finalDirection * 200, ForceMode.Impulse);
    }
}
