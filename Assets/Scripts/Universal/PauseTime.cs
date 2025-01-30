using UnityEngine;

public class PauseTime : MonoBehaviour
{
    public bool isPaused = false;

    public void FreezeTime()
    {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        isPaused = true;
    }

    public void ResumeTime()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        isPaused = false;
    }
}
