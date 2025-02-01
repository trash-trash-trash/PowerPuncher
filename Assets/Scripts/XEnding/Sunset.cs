using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class Sunset : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float colorLerpDuration = 60f;
    
    public Image sunImage; 
    private Color[] sunColors = { Color.yellow, new Color(1f, 0.5f, 0f), Color.red };
    
    private RectTransform sunRectTransform;

    public Image skyImage;
    private Color[] skyColors = { Color.blue, Color.red, Color.black };

    public RectTransform textRectTransform;
    
    public PlayerControls playerControls;

    private bool revealed = false;

    public GameObject replayButton;
    public GameObject quitButton;
    
    void Start()
    {
        sunRectTransform = sunImage.GetComponent<RectTransform>();

        playerControls.AnnounceQuit += AnyInputQuit;
        playerControls.AnnounceMovementVector2 += AnyInputWASD;
        playerControls.AnnounceLeftPunch += AnyInput;
        playerControls.AnnounceLeftPunch += AnyInput;
        
        StartCoroutine(SunsetRoutine());
    }

    private void AnyInputWASD(Vector2 obj)
    {
        RevealButtons();
    }

    private void AnyInputQuit()
    {
        RevealButtons();
    }

    private void AnyInput(InputAction.CallbackContext obj)
    {
        if(obj.canceled)
            RevealButtons();
    }

    public void RevealButtons()
    {
        revealed = true;
        replayButton.SetActive(true);
        quitButton.SetActive(true);
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private IEnumerator SunsetRoutine()
    {
        float elapsedTime = 0f;
        int colorIndex = 0;

        while (colorIndex < sunColors.Length - 1)
        {
            while (elapsedTime < colorLerpDuration)
            {
                float t = elapsedTime / colorLerpDuration;
                sunImage.color = Color.Lerp(sunColors[colorIndex], sunColors[colorIndex + 1], t);
                skyImage.color = Color.Lerp(skyColors[colorIndex], skyColors[colorIndex + 1], t);
                
                sunRectTransform.anchoredPosition -= new Vector2(0, moveSpeed / 2 * Time.deltaTime);
                textRectTransform.anchoredPosition += new Vector2(0, moveSpeed * 2 * Time.deltaTime);
                
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            
            elapsedTime = 0f;
            colorIndex++;
        }
        
        sunImage.color = sunColors[sunColors.Length - 1];
        skyImage.color = skyColors[skyColors.Length - 1];
        
        if (!revealed)
            RevealButtons();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        if (EditorApplication.isPlaying)
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
