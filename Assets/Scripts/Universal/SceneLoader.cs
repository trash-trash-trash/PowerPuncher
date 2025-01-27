using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Scene gameOverScene;
    public Scene mainScene;   
    
    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverScene.name);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene(mainScene.name);
    }
}
