using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPView : MonoBehaviour
{
    public HealthComponent HP;

    public TMP_Text HPText;

    public Slider HPSlider;
    
    void OnEnable()
    {
        HP.AnnounceHP += SetHP;
    }

    private void SetHP(HealthData obj)
    {
        HPSlider.value = obj.currentHP;
        HPSlider.maxValue = obj.maxHP;
        
        HPText.text = "HP: "+obj.currentHP;
    }

    void OnDisable()
    {
        HP.AnnounceHP -= SetHP;
    }
}
