using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string gameOverScene;
    public string mainScene;   
    
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverScene);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene);
    }
}
