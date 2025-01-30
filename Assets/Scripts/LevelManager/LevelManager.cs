using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public HealthComponent playerHP;

    public SceneLoader sceneLoader;

    public GameObject tutorialObj;

    void OnEnable()
    {
        tutorialObj.SetActive(true);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        playerHP.AnnounceHP += EndGame;
    }

    public void StartGame()
    {
        tutorialObj.SetActive(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

    private void EndGame(HealthData obj)
    {
            if(!obj.isAlive)
                sceneLoader.LoadGameOverScene();
    }
}
