using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    
    private string mainScene = "MainScene";  
    private string gameOverScene = "GameOver";
    private string youWinScene = "YouWin";
    
    public void LoadMainScene()
    { 
        SceneManager.LoadScene(mainScene);
    }
    
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void LoadYouWinScene()
    {
        SceneManager.LoadScene(youWinScene);
    }
}
