using System;
using System.Collections;
using UnityEngine;

public class PowerGauge : MonoBehaviour
{
    public int currentPower=0;
    public int desiredPowerLevel;

    public float numberOfMultipliers=1;
    public int maxNumberOfMultipliers = 30;
    
    public bool powerIncreasing=false;

    private IEnumerator addPowerCoro;
    
    public int currentLevel = 1;
    public int powerRequiredToLevelUp;
    
    public event Action<int> AnnouncePowerLevel;

    public event Action AnnounceLevelUp;

    void Start()
    {
        addPowerCoro = AddMotherfuckingPower();
    }

    public void IncreasePower()
    {
        int random = UnityEngine.Random.Range(420, 1420);
        ChangePower(random);
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

    public IEnumerator AddMotherfuckingPower()
    {
        powerIncreasing = true;
        while (currentPower < desiredPowerLevel)
        {
          //  currentPower++;
            
            int multiplierGroup = Mathf.FloorToInt(numberOfMultipliers / 10);  // Every 5 multipliers, increase by a power of 10

            // Increase power by 10^multiplierGroup
            float increment = Mathf.Pow(10, multiplierGroup);

            currentPower += (int)increment;
            
            yield return new WaitForSeconds(1f / numberOfMultipliers);
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
        currentLevel += 10;
        currentPower -= powerRequiredToLevelUp;

        int killsNeededForNextLevel = 5 * currentLevel * 2;
        powerRequiredToLevelUp = killsNeededForNextLevel * 1420;
        AnnounceLevelUp?.Invoke();
    }
}