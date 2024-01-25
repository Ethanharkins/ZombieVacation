using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
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
    }
}
