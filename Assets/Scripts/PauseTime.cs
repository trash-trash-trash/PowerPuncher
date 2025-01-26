using UnityEngine;

public class PauseTime : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Press 'P' to pause/resume
        {
            if (isPaused)
            {
                ResumeTime();
            }
            else
            {
                FreezeTime();
            }
        }
    }

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
