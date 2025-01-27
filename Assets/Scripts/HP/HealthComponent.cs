using System;
using System.Collections;
using UnityEngine;

[Serializable]
public struct HealthData
{
    public int maxHP;
    public int currentHP;
    public bool canTakeDamage;
    public bool isAlive;
}

public class HealthComponent : MonoBehaviour
{   
    public HealthData hpData;
    public int maxHP;

    public event Action<HealthData> AnnounceHP;

    void OnEnable()
    {
        hpData = new HealthData();
        StartCoroutine(HackWait());
    }

    IEnumerator HackWait()
    {
        yield return new WaitForFixedUpdate();
        
        Rez();
    }

    public void Rez()
    {
        hpData.maxHP = maxHP;
        hpData.canTakeDamage = true;
        ChangeHP(hpData.maxHP);
    }

    public void ChangeMaxHP(int maxHP)
    {
        hpData.maxHP = maxHP;
        hpData.currentHP = hpData.maxHP;
        AnnounceHP?.Invoke(hpData);
    }
    
    public void ChangeHP(int amount)
    {
        if (amount <= 0 && !hpData.canTakeDamage)
            return;
        
        int newCurrentHP = hpData.currentHP + amount;

        if (newCurrentHP <= 0)
        {
            hpData.currentHP = 0;
            hpData.isAlive = false;
        }
        else if (newCurrentHP > 0)
            hpData.isAlive = true;

        if (hpData.currentHP > hpData.maxHP)
            hpData.currentHP = hpData.maxHP;
        
        else
            hpData.currentHP = newCurrentHP;
        
        AnnounceHP?.Invoke(hpData);
    }
}
