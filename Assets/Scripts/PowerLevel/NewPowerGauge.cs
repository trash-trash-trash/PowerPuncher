using System;
using System.Collections;
using UnityEngine;

public class NewPowerGauge : MonoBehaviour
{
    public int killCounter;
    public int killsNeeded;
    public int level = 1;
    public float curveMultiplier = 0.3f;
    public int multiplier;
    public int maxMultiplier = 8;
    private int multiplierCounter;
    
    public int powerLevel = 1000000;
    
    public bool canTakePower = true;

    public event Action<int> AnnouncePowerLevel;

    void Start()
    {
        killsNeeded = 2;
    }

    public void IncreasePower()
    {
        killCounter++;
        if (killCounter >= killsNeeded)
        {
            killsNeeded += Mathf.CeilToInt(killsNeeded * curveMultiplier);
        }
        AddMultiplier();
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
}
