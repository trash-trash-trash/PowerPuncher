using System.Collections;
using UnityEngine;

public class BossSpawnState : BossStateBase
{
    public HealthComponent hp;
    public MoveToGround mTG;

    public int bossMaxHP;
    public int multiplier = 1;
    
    public override void OnEnable()
    {
        base.OnEnable();
        hp = GetComponentInParent<AIHP>();
        mTG = GetComponentInParent<MoveToGround>();
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForSeconds(0.5f);
        hp.ChangeMaxHP(bossMaxHP);
        StartCoroutine(Heal());
    }

    IEnumerator Heal()
    {
        while(hp.hpData.currentHP < hp.hpData.maxHP)
        {
            hp.ChangeHP(100 * multiplier);
            yield return new WaitForEndOfFrame();
            multiplier ++;
        }
        StartCoroutine(MoveToGroundCoro());
    }

    IEnumerator MoveToGroundCoro()
    {
        mTG.FlipMovingToGround(true);
        
        while (mTG.movingToGround)
            yield return new WaitForFixedUpdate();
        
        yield return new WaitForFixedUpdate();
        bossBrain.ChangeState(BossStates.TurnToPlayer);
    }
}
