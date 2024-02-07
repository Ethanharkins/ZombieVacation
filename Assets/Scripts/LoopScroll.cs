using UnityEngine;

public class LoopScroll : MonoBehaviour
{
    public float scrollSpeed = 5.0f; // Speed at which the object scrolls
    public float resetInterval = 10.0f; // Time in seconds before the object resets to its original position

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.position; // Save the original position of the object
    }

    void Update()
    {
        // Move the object backwards at scrollSpeed units per second
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);

        // Increment the timer by the time passed since last frame
        timer += Time.deltaTime;

        // Check if the timer has reached the reset interval
        if (timer >= resetInterval)
        {
            // Reset the object's position to its original position
            transform.position = startPosition;
            // Reset the timer
            timer = 0;
        }
    }
}
