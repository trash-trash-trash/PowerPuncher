using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour
{
    public HealthComponent HP;

    public TMP_Text HPText;

    public Slider HPSlider;

    public bool player = false;
    
    void OnEnable()
    {
        HP.AnnounceHP += SetHP;
    }

    private void SetHP(HealthData obj)
    {
        HPSlider.value = obj.currentHP;
        HPSlider.maxValue = obj.maxHP;
        
        if(player)
            HPText.text = "HP: "+obj.currentHP.ToString("N0");
        else
            HPText.text = "OMEGA NIPPER POWER: " + obj.currentHP.ToString("N0");
    }

    void OnDisable()
    {
        HP.AnnounceHP -= SetHP;
    }
}
