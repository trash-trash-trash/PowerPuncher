using System;
using System.Collections;
using UnityEngine;

public class PowerGauge : MonoBehaviour
{
    public int totalKillCount;
    public int killCounter;
    public int killsNeeded;
    public int level = 1;
    public float curveMultiplier = 0.3f;
    public int multiplier=3;
    public int maxMultiplier = 8;
    private int multiplierCounter;
    
    public int powerLevel=100;
    
    public bool canTakePower = true;

    public event Action AnnounceMaxLevel;

    public event Action<int, int> AnnounceKillCounter;
    
    public event Action<int> AnnouncePowerLevel;

    public event Action AnnounceLevelUp;

    void Start()
    {
        killsNeeded = 2;
    }

    public void IncreasePower()
    {
        killCounter++;
        totalKillCount++;
        if (killCounter >= killsNeeded)
        {
            killCounter = 0;
            float adjustedCurveMultiplier = curveMultiplier;

            if (totalKillCount < 50)
            {
                adjustedCurveMultiplier = Mathf.Max(curveMultiplier * 0.5f, 0.1f);
            }
            else if (totalKillCount % 50 == 0)
            {
                adjustedCurveMultiplier = Mathf.Max(curveMultiplier * 0.9f, 0.05f);
            }
            killsNeeded += Mathf.CeilToInt(killsNeeded * adjustedCurveMultiplier);
            AnnounceLevelUp?.Invoke();
        }
        AddMultiplier();
        AnnounceKillCounter?.Invoke(killCounter, killsNeeded);
    }

    void Update()
    {
        if (canTakePower)
        {
            powerLevel += (int)(Time.deltaTime * Mathf.Pow(10, multiplier - 1));
            AnnouncePowerLevel?.Invoke(powerLevel);
        }
    }

    public void AddMultiplier()
    {
        if (multiplier >= maxMultiplier)
            return;
        
        multiplierCounter++;
        
        if(multiplierCounter % 10==0) 
            multiplier++;
    }

    public void MaxLevel()
    {
        canTakePower = false;
        AnnounceMaxLevel?.Invoke();
    }
}