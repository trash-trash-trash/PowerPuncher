using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PunchView : MonoBehaviour
{
    public Punch punch;

    public float waitTime;

    public ParticleSystem explosionParticle;

    public Animator leftArmController;
    public Animator rightArmController;

    private Coroutine leftPunchCoro;
    private Coroutine rightPunchCoro;

    public Slider leftSlider;
    public Slider rightSlider;

    public Transform explosionTransform;
    
    public Transform leftPunchTransform;
    public Transform rightPunchTransform;
    
    void Start()
    {
        punch = gameObject.GetComponentInParent<Punch>();
        punch.AnnounceLeftRightPunch += PlayPunchAnimation;
        punch.AnnounceLeftRightTimer += SetTimer;
        punch.AnnouncePerfectPunch += PlayExplosion;
    }

    private void PlayExplosion(bool input)
    {
        if (input)
            explosionTransform.position = leftPunchTransform.position;
        else
            explosionTransform.position = rightPunchTransform.position;
            
        explosionParticle.Play();
    }
    
    private void SetTimer(bool input, float timer)
    {
        if (input)
        {
            leftSlider.value = timer;
            UpdateSliderColor(leftSlider, timer);
        }
        else
        {
            rightSlider.value = timer;
            UpdateSliderColor(rightSlider, timer);
        }
    }

    //false is right left is true
    private void PlayPunchAnimation(bool input)
    {
        if (input)
        {
            if (leftPunchCoro != null)
                StopCoroutine(leftPunchCoro);

            leftPunchCoro = StartCoroutine(LeftPunch());
        }
        else
        {
            if (rightPunchCoro != null)
                StopCoroutine(rightPunchCoro);

            rightPunchCoro = StartCoroutine(RightPunch());
        }
    }
    
    private void UpdateSliderColor(Slider slider, float timer)
    {
        Color targetColor;
            
        if (timer < punch.minSweetTime)
            targetColor = Color.Lerp(Color.white, Color.red, timer / 0.9f); 
        
        else if (timer >= punch.minSweetTime && timer <= punch.maxSweetTime) 
            targetColor = Color.yellow;
        
        else
            targetColor = Color.grey;

        slider.fillRect.GetComponent<Image>().color = targetColor;
    }
    
    //waitTime is hack, use punch duration when set up

    private IEnumerator LeftPunch()
    {
        leftArmController.Play("ArmsPunch");
        yield return new WaitForSeconds(waitTime);
        leftArmController.Play("ArmsIdle");
    }
    
    private IEnumerator RightPunch()
    {
        rightArmController.Play("ArmsPunch");
        yield return new WaitForSeconds(waitTime);
        rightArmController.Play("ArmsIdle");
    }

    void OnDisable()
    {
        punch.AnnounceLeftRightPunch -= PlayPunchAnimation;
        punch.AnnounceLeftRightTimer -= SetTimer;
        punch.AnnouncePerfectPunch -= PlayExplosion;
    }
}
