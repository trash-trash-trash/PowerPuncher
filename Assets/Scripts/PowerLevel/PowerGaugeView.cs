using System.Collections;
using TMPro;
using UnityEngine;

public class PowerGaugeView : MonoBehaviour
{
    public PowerGauge powerGauge;
    public TMP_Text powerText;
  
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
        powerText.text = "POWER LEVEL: "+powerLevel.ToString("N0");
    }

    void OnDisable()
    {
        powerGauge.AnnouncePowerLevel -= SetText;
    }
}
