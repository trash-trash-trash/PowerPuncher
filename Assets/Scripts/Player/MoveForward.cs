using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody rb;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;

    public PlayerControls controls;
    public Transform playerTransform;

    private Vector2 inputVector2;
    private Vector3 lastMousePosition;

    void OnEnable()
    {
        controls.AnnounceMovementVector2 += SetInputVector2;
    }

    private void SetInputVector2(Vector2 newInputVector)
    {
        inputVector2 = new Vector2(
            Mathf.Clamp(newInputVector.x, -1, 1),  // Clamp X-axis input
            Mathf.Clamp(newInputVector.y, -1, 1)   // Clamp Y-axis input
        );
    }

    void FixedUpdate()
    {
        if (inputVector2 != Vector2.zero)
        {
            Move();
        }

        RotateWithMouse();
    }

    void Move()
    {
        // Move forward/backward (Z-axis) and strafe left/right (X-axis)
        Vector3 moveDirection = (playerTransform.forward * inputVector2.y) +
                                (playerTransform.right * inputVector2.x);
        
        Vector3 force = moveDirection * acceleration * Time.fixedDeltaTime;
        rb.AddForce(force, ForceMode.Acceleration);

        // Clamp velocity to max speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void RotateWithMouse()
    {
        if (Time.timeScale > 0f)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false; // Hide cursor

            // Get mouse delta movement
            float mouseX = Input.GetAxis("Mouse X"); // Gets movement relative to last frame

            // Rotate around Y-axis
            float rotation = mouseX * rotationSpeed * Time.fixedDeltaTime;
            Quaternion newRotation = Quaternion.Euler(0, rotation, 0) * rb.rotation;
            rb.MoveRotation(newRotation);
        }
    }
}