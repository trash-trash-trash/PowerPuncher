using UnityEngine;

public class GrowShrink : MonoBehaviour
{
    public PowerGauge powerGauge;

    public Transform playerTransform;
    
    public float scalingFactor = 0.00001f;
    
    void OnEnable()
    {
     //   powerGauge.AnnouncePowerLevel += GrowPlayer;
    }

    private void GrowPlayer(int obj)
    {
        // Instead of directly multiplying the scale by obj, multiply by a fraction to make it gradual
        float scaleChange = Mathf.Pow(1 + scalingFactor, obj);  // Apply scaling in small increments
        
        // Apply the calculated scale change incrementally
        
        playerTransform.localScale *= scaleChange;
    }
}
