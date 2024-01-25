using UnityEngine;

public class ObjectScroll : MonoBehaviour
{
    public float scrollSpeed = 1.0f;
    private Vector3 originalPosition;
    public float resetThreshold = -10.0f;

    void Start()
    {
        originalPosition = transform.position;
        Debug.Log("Original Position: " + originalPosition);
    }

    void Update()
    {
        transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);

        Debug.Log("Current Position: " + transform.position);

        if (transform.position.z < resetThreshold)
        {
            Debug.Log("Resetting Position");
            transform.position = originalPosition;
        }
    }
}
