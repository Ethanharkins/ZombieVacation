using UnityEngine;

public class NewObjectScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    public float lifetime = 10.0f; // Lifetime of the object in seconds
    private float age = 0.0f; // Current age of the object

    void Update()
    {
        // Move the object
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);

        // Update the age of the object
        age += Time.deltaTime;

        // Check if the object's age exceeds its lifetime
        if (age > lifetime)
        {
            Destroy(gameObject); // Destroy the object
        }
    }
}
