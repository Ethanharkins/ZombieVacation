using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Get input from left and right arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");

        // Update target position
        targetPosition += Vector3.right * horizontalInput * speed * Time.deltaTime;

        // Move the front of the car immediately
        transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);

        // To make the back of the car follow with a delay, adjust the lerp speed
        // transform.position = Vector3.Lerp(transform.position, targetPosition, delaySpeed * Time.deltaTime);
    }
}
