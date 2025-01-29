using System.Collections;
using UnityEngine;

public class PlayerBackAnimator : MonoBehaviour
{
    public PlayerHP playerHP;
    
    public PowerupManager powerupManager;
    
    public Animator animator;
    
    public UIGrow uiGrow;

    public float powerupTime = 0.7f;

    void Start()
    {
        playerHP.AnnounceCanTakeDamage += TakeDamage;

        powerupManager.AnnounceFreezeTime += PowerupBack;
    }

    private void PowerupBack(bool input)
    {
        if (input)
        {
            animator.Play("Back_Powerup");
            uiGrow.Grow();
        }
        else
            StartCoroutine(PowerupCoro());
    }

    IEnumerator PowerupCoro()
    {
        yield return new WaitForSeconds(powerupTime);
        animator.Play("Back_Normal");
    }

    private void TakeDamage(bool input)
    {
        if(input)
            animator.Play("Back_TakeDamage");
        else    
            animator.Play("Back_Normal");
    }

    void OnDisable()
    {
        playerHP.AnnounceCanTakeDamage -= TakeDamage;
        powerupManager.AnnounceFreezeTime -= PowerupBack;
    }
}
