using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject retryButton;
    public GameObject quitButton;

    public GameObject dontQuitButton;
    public GameObject reallyQuitButton;

    public void Retry()
    {
        
    }

    public void Quit()
    {
        
    }
    
    public void DontQuit()
    {
        
    }
    
    public void ReallyQuit()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
