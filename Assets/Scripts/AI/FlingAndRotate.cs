using UnityEngine;

public class FlingAndRotate : MonoBehaviour
{
    public Rigidbody rb;

    public float upForce;
    public float horizontalForce;
    public float spinForce;
    public Vector3 direction;
    public Vector3 spinDirection;

    public void Explode(Rigidbody newRb)
    {
        rb = newRb;
        
        rb.constraints = RigidbodyConstraints.None;

        /*direction = new Vector3(0, 0, 0);
        direction.x = RandomBetween1And3();
        direction.z = RandomBetween1And3();
        
        rb.AddForce(direction * horizontalForce);*/

        Rotate();
    }

    public void Rotate()
    {
        spinDirection = new Vector3(0, 0, 0);
        spinDirection.x = RandomBetween1And3();
        spinDirection.z = RandomBetween1And3(); 
        spinDirection.y = RandomBetween1And3();
        rb.AddTorque(spinDirection * spinForce);
    }

    public int RandomBetween1And3()
    {
        int[] possibleValues = { -1, 0, 1 };
        int randInt = possibleValues[Random.Range(0, possibleValues.Length)];
        return randInt;
    }
}
