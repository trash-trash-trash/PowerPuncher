using System;
using System.Collections;
using UnityEngine;

public class PlayerHP : HealthComponent
{
    public float invincibilityTime = 0.3f;
    
    public event Action<bool> AnnounceCanTakeDamage;
    public override void ChangeHP(int amount)
    {
        base.ChangeHP(amount);
        if (amount < 0 && hpData.isAlive)
            StartCoroutine(TookDamage());
    }

    IEnumerator TookDamage()
    {
        hpData.canTakeDamage = true;
        AnnounceCanTakeDamage?.Invoke(hpData.canTakeDamage);

        yield return new WaitForSeconds(invincibilityTime);
        
        hpData.canTakeDamage = false;
        AnnounceCanTakeDamage?.Invoke(hpData.canTakeDamage);
    }
}
