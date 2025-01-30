using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{
    private PlayerInputs playerInputs;
    
    public event Action<InputAction.CallbackContext> AnnounceLeftPunch;
    public event Action<InputAction.CallbackContext> AnnounceRightPunch;
    public event Action<Vector2> AnnounceMovementVector2;

    public event Action AnnounceQuit;
    
    private void Awake()
    {
        playerInputs = new PlayerInputs();

        playerInputs.Keyboard.LeftPunch.performed += OnLeftPunch;
        playerInputs.Keyboard.LeftPunch.canceled += OnLeftPunch;
        playerInputs.Keyboard.RightPunch.performed += OnRightPunch;
        playerInputs.Keyboard.RightPunch.canceled += OnRightPunch;
        playerInputs.Keyboard.Movement.performed += OnWASDMovement;
        playerInputs.Keyboard.Movement.canceled += OnWASDMovement;
        playerInputs.Keyboard.Escape.canceled += OnQuit;
    }

    private void OnQuit(InputAction.CallbackContext context)
    {
        AnnounceQuit?.Invoke();
    }

    private void OnWASDMovement(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        AnnounceMovementVector2?.Invoke(movement);
    }

    private void OnEnable()
    {
        playerInputs.Keyboard.Enable();
    }
    
    private void OnLeftPunch(InputAction.CallbackContext context)
    {
        AnnounceLeftPunch?.Invoke(context);
    }
    
    private void OnRightPunch(InputAction.CallbackContext context)
    {
        AnnounceRightPunch?.Invoke(context);
    }   
    
    private void OnDisable()
         {
             playerInputs.Keyboard.LeftPunch.performed -= OnLeftPunch;
             playerInputs.Keyboard.LeftPunch.canceled -= OnLeftPunch;
             playerInputs.Keyboard.RightPunch.performed -= OnRightPunch;
             playerInputs.Keyboard.RightPunch.canceled -= OnRightPunch;
             playerInputs.Keyboard.Movement.performed -= OnWASDMovement;
             playerInputs.Keyboard.Movement.canceled -= OnWASDMovement;
             
             playerInputs.Keyboard.Disable();
         }

}