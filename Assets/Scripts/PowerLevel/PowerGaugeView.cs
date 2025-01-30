using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerGaugeView : MonoBehaviour
{
    public PowerGauge powerGauge;
    public TMP_Text powerText;

    public Slider powerSlider;
  
    public float shakeAmount;

    private Vector3 originalPosition;  

    private IEnumerator shakeRoutine;

    void Start()
    {
             originalPosition = powerText.rectTransform.position;
             powerGauge = gameObject.GetComponentInParent<PowerGauge>();
             powerGauge.AnnouncePowerLevel += SetText;
             StartCoroutine(ShakeCoroutine());
    }

    IEnumerator ShakeCoroutine()
    {
        while (true) 
        {
            if (powerGauge.powerIncreasing)
            {
                float xOffset = Random.Range(-shakeAmount, shakeAmount);
                float yOffset = Random.Range(-shakeAmount, shakeAmount);
                powerText.rectTransform.position = originalPosition + new Vector3(xOffset, yOffset, 0);
            }
            else
            {
                powerText.rectTransform.position = originalPosition;
            }

            yield return null;
        }
    }

    private void SetText(int powerLevel)
    {
        if (powerLevel == -420)
        {
            powerText.text = "POWER LEVEL: MAXIMUM!!";
            return;
        }

        powerText.text = "POWER LEVEL: "+powerLevel.ToString("N0");
        
        float progressPercent = (float)powerLevel / powerGauge.powerRequiredToLevelUp * 100f;
        powerSlider.maxValue = 100;
        powerSlider.value = progressPercent;
    }

    void OnDisable()
    {
        powerGauge.AnnouncePowerLevel -= SetText;
    }
}
