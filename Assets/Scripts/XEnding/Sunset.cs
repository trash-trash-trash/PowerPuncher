using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
    
    private float elapsedTime = 0f;

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
    }

    void Update()
    {
        sunRectTransform.anchoredPosition -= new Vector2(0, moveSpeed /2 * Time.deltaTime);
        textRectTransform.anchoredPosition += new Vector2(0, moveSpeed * 2* Time.deltaTime);
        
        elapsedTime += Time.deltaTime;
        float lerpFactor = (elapsedTime % (colorLerpDuration * 2)) / colorLerpDuration;
        int colorIndex = Mathf.FloorToInt(lerpFactor);
        float colorT = lerpFactor - colorIndex;
        
        if (colorIndex < sunColors.Length - 1)
        {
            sunImage.color = Color.Lerp(sunColors[colorIndex], sunColors[colorIndex + 1], colorT);
            skyImage.color = Color.Lerp(skyColors[colorIndex], skyColors[colorIndex + 1], colorT);
        }
        
        if(elapsedTime > colorLerpDuration *2&& !revealed)
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