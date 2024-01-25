using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float turnSpeed = 200.0f;

    void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate the car
        transform.Rotate(0, horizontalInput * turnSpeed * Time.deltaTime, 0);

        // Move the car forward based on its current rotation
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
