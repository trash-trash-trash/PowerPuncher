using UnityEngine;

public class MoveToSky : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public LayerMask skyLayer;

    public float skyY;
    public bool movingTosky = false;
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

    public void FlipMovingTosky(bool input)
    {
        movingTosky = input;

        if (input)
        {
            if (Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, Mathf.Infinity, skyLayer))
            {
                skyY = hit.point.y;
                movingTosky = true;
            }
        }
    }
    
    void Update()
    {
        if (movingTosky)
        {
            MoveTowardsky();
        }
    }

    private void MoveTowardsky()
    {
        float targetY = skyY - colliderHeightOffset; 
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
            movingTosky = false; 
        }
    }
}