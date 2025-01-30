using UnityEngine;

public class ArmKillCounter : MonoBehaviour
{
    public Punch punch;

    public PlayerPowerupMiddleman powerupMiddleman;

    public int leftArmKillCounter = 0;
    public int rightArmKillCounter = 0;

    public int leftArmNextLevelUpCounter = 1;
    public int rightArmNextLevelUpCounter = 1;

    private float killCap = 50;
    private int killMultiplierNormal = 2;
    private int killMultiplierAfterCap = 1;

    void Start()
    {
        punch.AnnouncePerfectPunch += IncreaseKillCount;
    }

    private void IncreaseKillCount(bool leftRight)
    {
        if(leftRight)
            leftArmKillCounter++;
        else
            rightArmKillCounter++;
        
        CheckKillCounter();
    }

    public void CheckKillCounter()
    {
        if (leftArmKillCounter >= leftArmNextLevelUpCounter)
        {
            int multiplier = 2;
            if (leftArmKillCounter > killMultiplierAfterCap)
                multiplier = killMultiplierNormal;
            else
                multiplier = killMultiplierAfterCap;
                
            leftArmNextLevelUpCounter *= multiplier;
            powerupMiddleman.IncreaseLeftArm();
        }
        else if (rightArmKillCounter >= rightArmNextLevelUpCounter)
        {  
                int multiplier = 2;
                if (rightArmKillCounter > killMultiplierAfterCap)
                    multiplier = killMultiplierNormal;
                else
                    multiplier = killMultiplierAfterCap;
            rightArmNextLevelUpCounter *= multiplier;
            powerupMiddleman.IncreaseRightArm();
        }
    }
}
