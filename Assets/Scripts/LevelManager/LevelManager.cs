using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public HealthComponent playerHP;

    public HealthComponent bossHP;

    public SceneLoader sceneLoader;

    public GameObject tutorialObj;

    public Pause pause;

    void OnEnable()
    {
        ShowTut(true);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        playerHP.AnnounceHP += EndGame;
        bossHP.AnnounceHP += WinGame;
    }

    private void WinGame(HealthData obj)
    {
        if(!obj.isAlive)
            sceneLoader.LoadYouWinScene();
    }

    public void ShowTut(bool input)
    {
        tutorialObj.SetActive(input);
    }

    public void StartGame()
    {ShowTut(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        pause.FlipCanQuit(true);
    }

    private void EndGame(HealthData obj)
    {
            if(!obj.isAlive)
                sceneLoader.LoadGameOverScene();
    }
}
