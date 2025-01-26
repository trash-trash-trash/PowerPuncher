using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerupManager : MonoBehaviour
{
    public bool isPaused = false;

    public GameObject powerGaugeObj;
    public GameObject leftArmSliderObj;
    public GameObject rightArmSliderObj;

    public GameObject powerupCanvasObj;
    public Transform powerupCanvasGridParentObj;
    public GameObject powerupCanvasObjPrefab;
    
    public List<GameObject> powerupObjs = new List<GameObject>();
    public List<Powerup> powerupList = new List<Powerup>();
    public Dictionary<PowerupsEnum, Powerup> powerups = new Dictionary<PowerupsEnum, Powerup>();
    
    public event Action<Powerup> AnnouncePowerup;

    void Start()
    {
        Powerup addMinimap = new Powerup();
        addMinimap.powerupType = PowerupsEnum.AddMiniMap;
        addMinimap.powerupName = "ADD MINIMAP";
        addMinimap.powerupDescription = "Adds a minimap to your HUD.";
        addMinimap.effectAmount = new List<float>();
        powerups.Add(PowerupsEnum.AddMiniMap, addMinimap);
        
        Powerup increaseLeftArm = new Powerup();
        increaseLeftArm.powerupType = PowerupsEnum.IncreaseLeftArm;
        increaseLeftArm.powerupName = "POWER UP LEFT ARM";
        increaseLeftArm.powerupDescription = "Increases your left arm's reach and power.";
        increaseLeftArm.effectAmount = new List<float>();
        increaseLeftArm.effectAmount.Add(1);
        powerups.Add(PowerupsEnum.IncreaseLeftArm, increaseLeftArm);
        
        Powerup increaseRightArm = new Powerup();
        increaseRightArm.powerupType = PowerupsEnum.IncreaseRightArm;
        increaseRightArm.powerupName = "POWER UP RIGHT ARM";
        increaseRightArm.powerupDescription = "Increases your right arm's reach and power.";
        increaseRightArm.effectAmount = new List<float>();
        increaseRightArm.effectAmount.Add(1);
        powerups.Add(PowerupsEnum.IncreaseRightArm, increaseRightArm);
        
        Powerup increaseMovementSpeed = new Powerup();
        increaseMovementSpeed.powerupType = PowerupsEnum.IncreaseMovementSpeed;
        increaseMovementSpeed.powerupName = "POWER UP LEGS";
        increaseMovementSpeed.powerupDescription = "Increases your movement speed.";
        increaseMovementSpeed.effectAmount = new List<float>();
        powerups.Add(PowerupsEnum.IncreaseMovementSpeed, increaseMovementSpeed);
        
        Powerup decreaseChargeTime = new Powerup();
        decreaseChargeTime.powerupType = PowerupsEnum.DecreaseChargeTime;
        decreaseChargeTime.powerupName = "POWER UP PUNCH SPEED";
        decreaseChargeTime.powerupDescription = "Decreases your punches' minimum charge time.";
        decreaseChargeTime.effectAmount = new List<float>();
        decreaseChargeTime.effectAmount.Add(1);
        powerups.Add(PowerupsEnum.DecreaseChargeTime, decreaseChargeTime);
        
        Powerup increaseSweetTime = new Powerup();
        increaseSweetTime.powerupType = PowerupsEnum.IncreaseSweetTime;
        increaseSweetTime.powerupName = "POWER UP PUNCH TIME";
        increaseSweetTime.powerupDescription = "Increases your punches' sweet time, making it easier to land.";
        increaseSweetTime.effectAmount = new List<float>();
        increaseSweetTime.effectAmount.Add(1);
        powerups.Add(PowerupsEnum.IncreaseSweetTime, increaseSweetTime);
        
        Powerup increaseEnemyAndScore = new Powerup();
        increaseEnemyAndScore.powerupType = PowerupsEnum.IncreaseEnemySize;
        increaseEnemyAndScore.powerupName = "POWER UP ENEMIES";
        increaseEnemyAndScore.powerupDescription = "Enemies are stronger but earn more points when killed.";
        increaseEnemyAndScore.effectAmount = new List<float>();
        increaseEnemyAndScore.effectAmount.Add(1);
        powerups.Add(PowerupsEnum.IncreaseEnemySize, increaseEnemyAndScore);
    }
    
    public void FreezeTime()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isPaused = true;
        
        powerGaugeObj.SetActive(false);
        leftArmSliderObj.SetActive(false);
        rightArmSliderObj.SetActive(false);
        
        powerupCanvasObj.SetActive(true);
        Spawn();
    }

    void Spawn()
    {
        int twoOrThree = GetRandomTwoOrThree();
        
        powerupList.Clear();

        foreach (Powerup pU in powerups.Values)
        {
            powerupList.Add(pU);
        }
        
        List<Powerup> powerupsToSpawn = new List<Powerup>();
        
        for (int i = 0; i < twoOrThree; i++)
        {
            int randomIndex = Random.Range(0, powerupList.Count);
            powerupsToSpawn.Add(powerupList[randomIndex]);
            powerupList.Remove(powerupList[randomIndex]);
        }

        for (int i = 0; i < powerupsToSpawn.Count; i++)
        {
            GameObject newObj = Instantiate(powerupCanvasObjPrefab, powerupCanvasGridParentObj);
            powerupObjs.Add(newObj);
            PowerupCanvasObj powerupCanvasObj = newObj.GetComponent<PowerupCanvasObj>();
            Powerup powerup = powerupsToSpawn[i];
            powerupCanvasObj.nameText.text = powerup.powerupName;
            powerupCanvasObj.descriptionText.text = powerup.powerupDescription;
            
            powerupCanvasObj.button.onClick.AddListener(() => AddPowerup(powerup));
            
            string result = "";
            if (powerup.effectAmount.Count > 0)
            {
                foreach (float value in powerup.effectAmount)
                {
                    string sign = value >= 0 ? "+" : "-";
                    result += $"{sign}{value:F2}\n";
                }
            }

            powerupCanvasObj.statsText.text = result;
            
            newObj.SetActive(true);
        }
    }

    public void AddPowerup(Powerup powerup)
    {
        AnnouncePowerup?.Invoke(powerup);

        if(powerup.powerupType == PowerupsEnum.AddMiniMap)
        {
            powerups.Remove(PowerupsEnum.AddMiniMap);
            
            Powerup increaseMinimap = new Powerup();
            increaseMinimap.powerupType = PowerupsEnum.IncreaseMinimap;
            increaseMinimap.powerupName = "INCREASE MINIMAP";
            increaseMinimap.powerupDescription = "Increases the range and size of your minimap.";
            increaseMinimap.effectAmount = new List<float>();
            powerups.Add(PowerupsEnum.IncreaseMinimap, increaseMinimap);
        }
    }

    public void Reset()
    {
        foreach (GameObject obj in powerupObjs)
        {
            Destroy(obj);
        }
        powerupObjs.Clear();
    }

    public int GetRandomTwoOrThree()
    {
        return Random.Range(0, 2) == 0 ? 2 : 3;
    }
    

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isPaused = false;
        Reset();
        powerupCanvasObj.SetActive(false);
        
        powerGaugeObj.SetActive(true);
        leftArmSliderObj.SetActive(true);
        rightArmSliderObj.SetActive(true);
    }
}
