using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public HealthComponent playerHP;

    public HealthComponent bossHP;

    public SceneLoader sceneLoader;

    public GameObject titleScreenObj;

    public GameObject tutorialObj;

    public MusicPlayer mp;
    
    public Pause pause;

    void OnEnable()
    {
        ShowTitle(true);
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        playerHP.AnnounceHP += EndGame;
        bossHP.AnnounceHP += WinGame;
    }

    public void ShowTitle(bool input)
    {
        titleScreenObj.SetActive(input);
    }

    public void ShowTutorial()
    {
        titleScreenObj.SetActive(false);
        ShowTut(true);
    }

    public void ShowTut(bool input)
    {
        tutorialObj.SetActive(input);
    }

    public void StartGame()
    {
        ShowTut(false);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        pause.FlipCanQuit(true);
        mp.StartSong();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void EndGame(HealthData obj)
    {
        if(!obj.isAlive) 
            sceneLoader.LoadGameOverScene();
    }   
    
    private void WinGame(HealthData obj)
    {
        if(!obj.isAlive)
            sceneLoader.LoadYouWinScene();
    }
}
