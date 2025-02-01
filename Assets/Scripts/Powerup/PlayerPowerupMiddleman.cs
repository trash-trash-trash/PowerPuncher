using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerupMiddleman : MonoBehaviour
{
    public PowerupManager powerup;

    public HealthComponent playerHP;

    public UIGrow HPSliderRect;
    
    public Punch punch;
    public MoveForward movement;
    
    public GameObject miniMapObj;
    public RectTransform miniMapRect;
    public Camera miniMapCamera;
    
    public RectTransform leftArmImage;
    public RectTransform rightArmImage;

    private float maxSpeedIncrement=100;

    public UIGrow miniMapUI;
    public UIArmsGrow leftArmUI;
    public UIArmsGrow rightArmUI;

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
            { PowerupsEnum.IncreaseMinimap , IncreaseMinimap},
            { PowerupsEnum.IncreaseMaxHPAndHeal , IncreaseMaxHPAndHeal}
        };
        powerup.AnnouncePowerup += ApplyPowerup;
    }
    
    public void AddMinimap()
    {
        miniMapObj.SetActive(true);
    }

    public void IncreaseMinimap()
    {
        miniMapUI.Grow();
        miniMapCamera.orthographicSize += 2;
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
        leftArmUI.Grow();
        punch.leftArmDmg -= 100; 
        punch.sweetPunchRadius += .1f;
       
        int newMaxHP = playerHP.hpData.maxHP += 100;
        
        playerHP.ChangeMaxHP(newMaxHP);
    }

    public void IncreaseRightArm()
    {
        rightArmUI.Grow();
        punch.rightArmDmg -= 100;
        punch.sweetPunchRadius += .1f;
        
        int newMaxHP = playerHP.hpData.maxHP += 100;
        
        playerHP.ChangeMaxHP(newMaxHP);
    }

    public void IncreaseMaxHPAndHeal()
    {
         int newMaxHP = playerHP.hpData.maxHP += 100;
         playerHP.ChangeMaxHP(newMaxHP);
         playerHP.ChangeHP(playerHP.hpData.maxHP);
    }
}