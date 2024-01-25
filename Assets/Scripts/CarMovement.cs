using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;     // Movement speed of the car
    public float leftBoundary = -5.0f; // Left boundary of movement
    public float rightBoundary = 5.0f; // Right boundary of movement

    void Update()
    {
        // Get horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate new position within boundaries
        float newXPosition = Mathf.Clamp(transform.position.x + horizontalInput * moveSpeed * Time.deltaTime, leftBoundary, rightBoundary);

        // Update the car's position
        transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }
}
