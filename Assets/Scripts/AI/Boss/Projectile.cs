using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(HackWaitToDie());
    }

    IEnumerator HackWaitToDie()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
            if (collision.gameObject.GetComponentInParent<PlayerHP>() != null)
            {
                PlayerHP playerHP = collision.gameObject.GetComponentInParent<PlayerHP>();
                playerHP.ChangeHP(-10);
                gameObject.SetActive(false);
            }
    }
}
