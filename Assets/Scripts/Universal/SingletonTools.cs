using UnityEngine;

public class SingletonTools : MonoBehaviour
{
    public static SingletonTools Instance { get; private set; }
    
    public FlingAndRotate flingAndRotate;

    public PowerGauge powerGauge;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }
}