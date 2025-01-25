using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public Rigidbody rb;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;

    public PlayerControls controls;

    public Transform playerTransform;
    
    public Vector2 inputVector2;

    void OnEnable()
    {
        controls.AnnounceMovementVector2 += SetInputVector2;
    }

    private void SetInputVector2(Vector2 newInputVector)
    {
        if (newInputVector.x > 0)
            newInputVector.x = 1;
        else if (newInputVector.x < 0)
            newInputVector.x = -1;
        if(newInputVector.y > 0)
            newInputVector.y = 1;
        else if (newInputVector.y < 0)
            newInputVector.y = -1;
        
        inputVector2 = newInputVector;
    }
    void FixedUpdate()
        {
            if (inputVector2 != Vector2.zero)
            {
               MoveForwardBack();
               RotateLeftRight();
            } 
        }

    void MoveForwardBack()
    {
        // Move forward/backward based on the Y-axis input
        Vector3 direction = playerTransform.forward * inputVector2.y; // Use Z-axis for forward movement
        Vector3 force = direction * acceleration * Time.fixedDeltaTime;

        rb.AddForce(force, ForceMode.Acceleration);

        // Clamp the velocity to max speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    void RotateLeftRight()
    {
        // Rotate around the Y-axis based on the X-axis input
        float rotation = inputVector2.x * rotationSpeed * Time.fixedDeltaTime;

        // Apply torque for smooth rotation
        rb.AddTorque(Vector3.up * rotation, ForceMode.Acceleration);

        // Limit angular velocity (optional for better control)
        if (rb.angularVelocity.magnitude > maxSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxSpeed;
        }
    }
}