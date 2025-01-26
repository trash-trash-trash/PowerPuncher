using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;

    public float yOffset;

    void FixedUpdate()
    {
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y + yOffset, playerTransform.position.z);
        
        // Ensure the camera's rotation matches the player's y rotation, keeping it fixed at 90 degrees on the X axis (to look down)
        transform.rotation = Quaternion.Euler(90f, playerTransform.eulerAngles.y, 0f);
    }
}
