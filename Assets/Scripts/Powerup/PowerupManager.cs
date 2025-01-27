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

    public Sprite movementSprite;
    public Sprite addMinimapSprite;
    public Sprite increaseMinimapSprite;
    public Sprite increasePunchMaxSweetSprite;
    public Sprite decreasePunchMinSweetSprite;
    
    public event Action<Powerup> AnnouncePowerup;

    void Start()
    {
        Powerup addMinimap = new Powerup();
        addMinimap.powerupType = PowerupsEnum.AddMiniMap;
        addMinimap.powerupName = "ADD MINIMAP";
        addMinimap.powerupDescription = "Adds a minimap to your HUD.";
        addMinimap.effects = "<color=#00FF00>+1 Minimap</color>";
        addMinimap.sprite = addMinimapSprite;
        powerups.Add(PowerupsEnum.AddMiniMap, addMinimap);
        
        Powerup increaseMovementSpeed = new Powerup();
        increaseMovementSpeed.powerupType = PowerupsEnum.IncreaseMovementSpeed;
        increaseMovementSpeed.powerupName = "POWER UP LEGS";
        increaseMovementSpeed.powerupDescription = "Increases your movement speed.";
        
     //   exampleText.text = "This is <color=#FF0000>red</color>\nThis is <color=#00FF00>green</color>\nThis is <color=#0000FF>blue</color>";

        increaseMovementSpeed.effects = "<color=#00FF00>+5 Movement Speed\n+5 Acceleration</color>";
        increaseMovementSpeed.sprite = movementSprite;
        powerups.Add(PowerupsEnum.IncreaseMovementSpeed, increaseMovementSpeed);
        
        //change this to left/right
        Powerup decreaseChargeTime = new Powerup();
        decreaseChargeTime.powerupType = PowerupsEnum.DecreaseChargeTime;
        decreaseChargeTime.powerupName = "POWER UP PUNCH SPEED";
        decreaseChargeTime.powerupDescription = "Decreases your punches' minimum charge time, letting you punch faster.";
        decreaseChargeTime.effects = "<color=#00FF00>-0.1 Punch Charge Time</color>";
        decreaseChargeTime.sprite = decreasePunchMinSweetSprite;
        powerups.Add(PowerupsEnum.DecreaseChargeTime, decreaseChargeTime);
        
        Powerup increaseSweetTime = new Powerup();
        increaseSweetTime.powerupType = PowerupsEnum.IncreaseSweetTime;
        increaseSweetTime.powerupName = "POWER UP PUNCH TIME";
        increaseSweetTime.powerupDescription = "Increases your punches' sweet time, making it easier to land.";
        increaseSweetTime.effects = "<color=#00FF00>+0.1 Punch Max Sweet Time</color>";
        increaseSweetTime.sprite = increasePunchMaxSweetSprite;
        powerups.Add(PowerupsEnum.IncreaseSweetTime, increaseSweetTime);
        
        Powerup increaseEnemyAndScore = new Powerup();
        increaseEnemyAndScore.powerupType = PowerupsEnum.IncreaseEnemySize;
        increaseEnemyAndScore.powerupName = "POWER UP ENEMIES";
        increaseEnemyAndScore.powerupDescription = "Enemies are stronger but earn more points when killed.";
        increaseEnemyAndScore.effects = "<color=#FF0000>+1 Enemy Size</color>\n<color=#00FF00>+ 5% more Power per kill</color>";
        powerups.Add(PowerupsEnum.IncreaseEnemySize, increaseEnemyAndScore);
        
        Powerup increaseMaxHP = new Powerup();
        increaseMaxHP.powerupType = PowerupsEnum.IncreaseMaxHPAndHeal;
        increaseMaxHP.powerupName = "POWER UP HEALTH";
        increaseMaxHP.powerupDescription = "Increases your Max HP and heals you to full."; 
        increaseMaxHP.effects = "<color=#00FF00>-0.1 Punch Charge Time</color>";
        powerups.Add(PowerupsEnum.IncreaseMaxHPAndHeal, increaseMaxHP);
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
            powerupCanvasObj.statsText.text = powerup.effects;
            
            if(powerup.sprite!=null)
                powerupCanvasObj.buttonImage.sprite = powerup.sprite;
            
            powerupCanvasObj.button.onClick.AddListener(() => AddPowerup(powerup));
            
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
            increaseMinimap.effects = "<color=#00FF00>+20 Minimap Range\n+1.1% Minimap Size</color>";
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
