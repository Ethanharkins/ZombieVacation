using UnityEngine;

public class BackPartFollow : MonoBehaviour
{
    public Transform car;
    public float followSpeed = 0.1f;
    public float jiggleSpeed = 1.0f; // Speed of the jiggle
    public float jiggleAngle = 4.0f; // Maximum angle of the jiggle in degrees

    private float lastXPosition;

    void Start()
    {
        lastXPosition = car.position.x;
    }

    void Update()
    {
        // Follow the car's position with a delay
        Vector3 newPosition = new Vector3(car.position.x, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, followSpeed * Time.deltaTime);

        // Jiggle effect on rotation when changing directions
        if (Mathf.Abs(car.position.x - lastXPosition) > 0.01f) // Threshold to detect change in direction
        {
            // Calculate jiggle rotation using sine wave
            float jiggle = Mathf.Sin(Time.time * jiggleSpeed) * jiggleAngle;
            transform.rotation = Quaternion.Euler(0, jiggle, 0);
        }
        else
        {
            // Reset to original rotation
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        lastXPosition = car.position.x; // Update the last position for the next frame
    }
}
