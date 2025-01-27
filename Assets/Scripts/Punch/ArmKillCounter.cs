using UnityEngine;

public class ArmKillCounter : MonoBehaviour
{
    public Punch punch;

    public PlayerPowerupMiddleman powerupMiddleman;

    public int leftArmKillCounter = 0;
    public int rightArmKillCounter = 0;

    public int leftArmNextLevelUpCounter = 1;
    public int rightArmNextLevelUpCounter = 1;

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
            leftArmNextLevelUpCounter *= 2;
            powerupMiddleman.IncreaseLeftArm();
        }
        else if (rightArmKillCounter >= rightArmNextLevelUpCounter)
        {
            rightArmNextLevelUpCounter *= 2;
            powerupMiddleman.IncreaseRightArm();
        }
    }
}
