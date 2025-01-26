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
    
    public event Action<int> AnnouncePowerLevel;

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
            
            int displayPower = currentPower;

            if (multiplierGroup >= 1)  // For values 10 and higher
            {
                // Generate a random digit between 0 and 9 for each decimal place
                int randomDecimals = 0;
                for (int i = 0; i < multiplierGroup; i++)
                {
                    randomDecimals = randomDecimals * 10 + UnityEngine.Random.Range(0, 10);  // Build up the random number
                }
                displayPower = currentPower * 10 + randomDecimals;  // Shift decimal place and add random digits
            }
            
            yield return new WaitForSeconds(1f / numberOfMultipliers);
            AnnouncePowerLevel?.Invoke(displayPower);
        }
        
        if(currentPower > desiredPowerLevel)
            currentPower = desiredPowerLevel;

        AnnouncePowerLevel?.Invoke(currentPower);
        numberOfMultipliers = 1;
        powerIncreasing = false;
    }
}