using System;
using System.Collections;
using UnityEngine;

public class PowerGauge : MonoBehaviour
{
    public int killCounter = 0;
    public int currentPower=0;
    public int desiredPowerLevel;

    public float numberOfMultipliers=1;
    public int maxNumberOfMultipliers = 30;
    
    public bool powerIncreasing=false;

    private IEnumerator addPowerCoro;
    
    public int currentLevel = 1;
    public int powerRequiredToLevelUp;

    public bool canTakePower = true;

    public event Action AnnounceMaxLevel;
    
    public event Action<int> AnnouncePowerLevel;

    public event Action AnnounceLevelUp;

    void Start()
    {
        addPowerCoro = AddMotherfuckingPower();
    }

    public void IncreasePower(int multiplier)
    {
        if (!canTakePower)
            return;
        
        killCounter++;
        int random = UnityEngine.Random.Range(1, 10);
        ChangePower(random * multiplier);
    }
    
    public void ChangePower(int input)
    {
        int newPower = desiredPowerLevel + input;

        if (newPower <= 0)
            newPower = 0;
        
        desiredPowerLevel = newPower;

        if (currentPower < desiredPowerLevel)
        {
            if (powerIncreasing)
            {
                StopCoroutine(addPowerCoro);
                StartCoroutine(AddMultiplier());
                numberOfMultipliers++;
                addPowerCoro = AddMotherfuckingPower();
                StartCoroutine(addPowerCoro);
            }
            else
            {
                addPowerCoro = AddMotherfuckingPower();
                StartCoroutine(addPowerCoro);
            }
        }
    }

    IEnumerator AddMultiplier()
    {
        yield return new WaitForSeconds(20f);
        numberOfMultipliers--;

        if (numberOfMultipliers <= 0)
            numberOfMultipliers = 0;
    }

    public IEnumerator AddMotherfuckingPower()
    {
        powerIncreasing = true;
        while (currentPower < desiredPowerLevel)
        {
            int powerGap = desiredPowerLevel - currentPower;
        
            int exponent = Mathf.FloorToInt(Mathf.Log10(powerGap));
            float increment = Mathf.Pow(10, exponent) * (1 + numberOfMultipliers * 0.1f);
        
            currentPower += Mathf.CeilToInt(increment);

            float speedFactor = Mathf.Pow(10, exponent) / 100f; 
            float waitTime = Mathf.Max(0.001f, 1f / (numberOfMultipliers * speedFactor)); 

            yield return new WaitForSeconds(waitTime);
            AnnouncePowerLevel?.Invoke(currentPower);
            
            if (currentPower >= powerRequiredToLevelUp)
            {
                LevelUp();
            }
        }
        
        if(currentPower > desiredPowerLevel)
            currentPower = desiredPowerLevel;

        AnnouncePowerLevel?.Invoke(currentPower);
        powerIncreasing = false;
    }

    private void LevelUp()
    {
        currentLevel += 1;
        currentPower -= powerRequiredToLevelUp;

        int killsNeededForNextLevel = 10 * currentLevel;
        powerRequiredToLevelUp = killsNeededForNextLevel * 100;
        AnnounceLevelUp?.Invoke();
    }

    public void MaxLevel()
    {
        canTakePower = false;
        AnnounceMaxLevel?.Invoke();
        powerIncreasing = true;
    }
}