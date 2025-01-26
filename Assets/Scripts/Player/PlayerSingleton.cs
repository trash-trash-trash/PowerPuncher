using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    public Transform playerCapsule;
    
    public static PlayerSingleton Instance { get; private set; }

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