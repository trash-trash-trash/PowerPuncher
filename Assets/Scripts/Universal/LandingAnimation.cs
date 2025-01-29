using System.Collections;
using UnityEngine;

public class LandingAnimation : MonoBehaviour
{
    public Vector3 scaleIncreaseRate = new Vector3(0.1f, 0.1f, 0.1f); // Change per second

    public SpriteRenderer spr;
    public float fadeRate = 0.5f;

    void Update()
    {
        transform.localScale += scaleIncreaseRate * Time.deltaTime;
        
        Color color = spr.color;
        color.a = Mathf.Max(0, color.a - fadeRate * Time.deltaTime);
        spr.color = color;

        if (spr.color.a <= 0)
            KillSelf();
    }

    void KillSelf()
    {
        Destroy(gameObject);
    }
}
