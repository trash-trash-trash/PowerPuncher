using UnityEngine;

public class MoveToGround : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public LayerMask groundLayer;

    public float groundY;
    public bool movingToGround = false;
    private float currentSpeed = 0f;
    public float colliderHeightOffset;

    void OnEnable()
    {
        if (TryGetComponent(out Collider collider))
        {
            colliderHeightOffset = collider.bounds.size.y/2;
        }
        else
        {
            Debug.LogWarning("No collider found on the object! Defaulting offset to 0.");
        }
    }

    public void FlipMovingToGround(bool input)
    {
        movingToGround = input;

        if (input)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                groundY = hit.point.y;
                movingToGround = true;
            }
        }
    }
    
    void Update()
    {
        if (movingToGround)
        {
            MoveTowardGround();
        }
    }

    private void MoveTowardGround()
    {
        float targetY = groundY + colliderHeightOffset; 
        float distance = Mathf.Abs(transform.position.y - targetY);

        currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

        float step = currentSpeed * Time.deltaTime;
        transform.position = new Vector3(
            transform.position.x,
            Mathf.MoveTowards(transform.position.y, targetY, step), 
            transform.position.z
        );

        if (distance <= 0.5f)
        {
            currentSpeed = 0f; 
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            movingToGround = false; 
        }
    }
}
