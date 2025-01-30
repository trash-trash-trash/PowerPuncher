using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject retryButton;
    public GameObject quitButton;

    public GameObject dontQuitButton;
    public GameObject reallyQuitButton;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    
    public void Retry()
    {
        
    }

    public void Quit()
    {
        retryButton.SetActive(false);
        quitButton.SetActive(false);
        dontQuitButton.SetActive(true);
        reallyQuitButton.SetActive(true);
    }
    
    public void DontQuit()
    {
        retryButton.SetActive(true);
        quitButton.SetActive(true);
        dontQuitButton.SetActive(false);
        reallyQuitButton.SetActive(false);
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
