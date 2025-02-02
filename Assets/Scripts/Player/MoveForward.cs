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
            Mathf.Clamp(newInputVector.x, -1, 1),  
            Mathf.Clamp(newInputVector.y, -1, 1)   
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
        Vector3 moveDirection = (playerTransform.forward * inputVector2.y) +
                                (playerTransform.right * inputVector2.x);
        
        Vector3 force = moveDirection * acceleration * Time.fixedDeltaTime;
        rb.AddForce(force, ForceMode.Acceleration);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void RotateWithMouse()
    {
            float mouseX = Input.GetAxis("Mouse X"); 

            float rotation = mouseX * rotationSpeed * Time.fixedDeltaTime;
            Quaternion newRotation = Quaternion.Euler(0, rotation, 0) * rb.rotation;
            rb.MoveRotation(newRotation);
    }
}