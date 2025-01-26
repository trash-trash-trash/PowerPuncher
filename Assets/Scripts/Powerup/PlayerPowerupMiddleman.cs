using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupMiddleman : MonoBehaviour
{
    public PowerupManager powerup;
    
    public Punch punch;
    public MoveForward movement;
    
    public GameObject miniMapObj;
    public RectTransform miniMapRect;
    public Camera miniMapCamera;
    
    public RectTransform leftArmImage;
    public RectTransform rightArmImage;

    public float maxSpeedIncrement=5;

    private Dictionary<PowerupsEnum, Action> powerupActions;

    void Start()
    {
        // Initialize the dictionary
        powerupActions = new Dictionary<PowerupsEnum, Action>
        {
            { PowerupsEnum.IncreaseLeftArm , IncreaseLeftArm},
            { PowerupsEnum.IncreaseRightArm , IncreaseRightArm},
            { PowerupsEnum.IncreaseMovementSpeed, IncreaseMoveSpeed },
            { PowerupsEnum.DecreaseChargeTime, DecreasePunchTimer },
            { PowerupsEnum.IncreaseSweetTime, IncreaseSweetTime },
            { PowerupsEnum.IncreaseEnemySize , IncreaseEnemySize},
            { PowerupsEnum.AddMiniMap , AddMinimap},
            { PowerupsEnum.IncreaseMinimap , IncreaseMinimap}
        };
        powerup.AnnouncePowerup += ApplyPowerup;
    }

    public void AddMinimap()
    {
        miniMapObj.SetActive(true);
    }

    public void IncreaseMinimap()
    {
        miniMapRect.localScale *= 1.01f;
        miniMapCamera.orthographicSize ++;
    }

    public void IncreaseEnemySize()
    {
        Debug.Log("Does nothing!");
    }

    // Example of using the dictionary to apply a power-up
    public void ApplyPowerup(Powerup newPower)
    {
        PowerupsEnum newPowerup = newPower.powerupType;
        
        if (powerupActions.TryGetValue(newPowerup, out var action))
        {
            action.Invoke();
            powerup.ResumeTime();
        }
        else
        {
            Debug.LogWarning($"No action found for power-up: {powerup}");
        }
    }
    
    public void IncreaseMoveSpeed()
    {
        movement.maxSpeed += maxSpeedIncrement;
        movement.acceleration += maxSpeedIncrement;
    }

    public void DecreasePunchTimer()
    {
        punch.minSweetTime += -0.1f;
        punch.maxSweetTime += 0.1f;
    }

    public void IncreaseSweetTime()
    {
        punch.maxSweetTime+= 0.1f;
    }

    public void IncreaseLeftArm()
    { 
        leftArmImage.localScale *= 1.1f;
        punch.leftArmDmg -= 20; 
        punch.leftArmPunchForce += 20f;
    }

    public void IncreaseRightArm()
    {
        rightArmImage.localScale *= 1.1f;
        punch.rightArmDmg -= 20;
        punch.rightArmPunchForce += 20f;
    }
}