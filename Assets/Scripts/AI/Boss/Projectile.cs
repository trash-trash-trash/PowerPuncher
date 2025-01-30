using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has a Rigidbody (use it for more specific checks)
        if (collision.rigidbody != null)
            if (collision.gameObject.GetComponentInParent<PlayerHP>() != null)
            {
                PlayerHP playerHP = collision.gameObject.GetComponentInParent<PlayerHP>();
                playerHP.ChangeHP(-10);
                gameObject.SetActive(false);
            }
    
    }
}
