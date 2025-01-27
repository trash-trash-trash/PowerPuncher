using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public HealthComponent playerHP;

    void OnEnable()
    {
        playerHP.AnnounceHP += EndGame;
    }

    private void EndGame(HealthData obj)
    {
            
    }
}
