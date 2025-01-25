using System.Collections;
using TMPro;
using UnityEngine;

public class PowerGaugeView : MonoBehaviour
{
    public PowerGauge powerGauge;
    public TMP_Text powerText;
    void Start()
    {
        powerGauge = gameObject.GetComponentInParent<PowerGauge>();
        powerGauge.AnnouncePowerLevel += SetText;
    }

    private void SetText(int powerLevel)
    {
        powerText.text = "POWER LEVEL: "+powerLevel.ToString("N0");
    }

    void OnDisable()
    {
        powerGauge.AnnouncePowerLevel -= SetText;
    }
}
