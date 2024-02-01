using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float driftAngle = 15.0f; // Maximum angle the car will rotate when drifting
    public float driftReturnSpeed = 2.0f; // How quickly the car returns to facing forward
    public float leftBoundary = -5.0f;
    public float rightBoundary = 5.0f;

    void Update()
    {
        // Get horizontal input (e.g., arrow keys, A/D keys)
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate new position within boundaries
        float newXPosition = Mathf.Clamp(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, leftBoundary, rightBoundary);

        // Update the car's position
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);

        // Rotate the car on the Y-axis based on horizontal input to simulate drifting
        RotateForDrift(horizontalInput);
    }

    void RotateForDrift(float input)
    {
        if (Mathf.Abs(input) > 0)
        {
            // Calculate target Y-axis rotation based on input direction
            float targetYRotation = driftAngle * input;
            // Smoothly rotate the car towards the target rotation
            Quaternion targetRotation = Quaternion.Euler(0, targetYRotation, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * driftReturnSpeed);
        }
        else
        {
            // Gradually return the car to neutral rotation when not moving left or right
            Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * driftReturnSpeed);
        }
    }
}
