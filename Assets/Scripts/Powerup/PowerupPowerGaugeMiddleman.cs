using UnityEngine;

public class PowerupPowerGaugeMiddleman : MonoBehaviour
{
    public PowerGauge powerGauge;
    
    public PowerupManager powerupManager;
    public void Start()
    {
        powerGauge.AnnounceLevelUp += ObtainNewPower;
    }

    private void ObtainNewPower()
    {
        powerupManager.FreezeTime();
    }

    void OnDisable()
    {
        powerGauge.AnnounceLevelUp -= ObtainNewPower;
    }
}
