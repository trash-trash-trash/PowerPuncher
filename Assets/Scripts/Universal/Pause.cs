using UnityEngine;

public class Pause : MonoBehaviour
{
  public PlayerControls playerControls;

  public GameObject pauseCanvasObj;

  public bool canQuit = false;
  
  public bool paused = false;
  
  void Start()
  {
     playerControls.AnnounceQuit += OpenPauseMenu;
  }

  public void FlipCanQuit(bool input)
  {
      canQuit = input;
  }

  public void ClosePauseMenu()
  {
      OpenPauseMenu();
  }

  private void OpenPauseMenu()
  {
      if (!canQuit)
          return;
      
      paused = !paused;
      if (paused)
      { 
          Cursor.visible = true;
          Cursor.lockState = CursorLockMode.None;
        
          Time.timeScale = 0f;
          Time.fixedDeltaTime = 0.02f * Time.timeScale;
      }
      else
      {
          Time.timeScale = 1f;
          Time.fixedDeltaTime = 0.02f;
      }
      pauseCanvasObj.SetActive(paused);
  }
}
