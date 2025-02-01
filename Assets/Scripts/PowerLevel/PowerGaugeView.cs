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

    private bool shaking = false;
    private IEnumerator shakeRoutine;

    void Start()
    {
             originalPosition = powerText.rectTransform.position;
             powerGauge = gameObject.GetComponentInParent<PowerGauge>();
             powerGauge.AnnouncePowerLevel += SetText;
             powerGauge.AnnounceMaxLevel += SetMaxText;
             powerGauge.AnnounceKillCounter += SetLevelUpGauge;
             shakeRoutine = ShakeCoroutine();
    }

    private void SetMaxText()
    {
        powerText.text = "POWER LEVEL:                 MAXIMUM!!";
    }

    IEnumerator ShakeCoroutine()
    {
        shaking = true;
        while (shaking) 
        {
            float xOffset = Random.Range(-shakeAmount, shakeAmount);
            float yOffset = Random.Range(-shakeAmount, shakeAmount);
            powerText.rectTransform.position = originalPosition + new Vector3(xOffset, yOffset, 0);

            yield return null;
        }
        
        powerText.rectTransform.position = originalPosition;
    }

    private void SetText(int powerLevel)
    {
        powerText.text = "POWER LEVEL: "+powerLevel.ToString("N0");
    }

    private void SetLevelUpGauge(int newKillCounter, int newKillsNeeded)
    {
        if(!shaking)
            StartCoroutine(shakeRoutine);
        
        float progressPercent = (float)newKillCounter / newKillsNeeded * 100f;
        powerSlider.maxValue = 100;
        powerSlider.value = progressPercent;
    }

    void OnDisable()
    {
        powerGauge.AnnouncePowerLevel -= SetText;
        powerGauge.AnnounceMaxLevel -= SetMaxText;
        powerGauge.AnnounceKillCounter -= SetLevelUpGauge;
    }
}
