using UnityEngine;

public class ObjectScroll : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 5f; // Now adjustable from the Inspector
    public float resetInterval = 100f; // Distance after which the object will reset its position
    private Vector3 startPosition;
    private float distanceScrolled;

    void Start()
    {
        // Optionally override the Inspector value with a value from GameManager, if needed
        // scrollSpeed = GameManager.Instance.GetCurrentScrollSpeed();
        startPosition = transform.position; // Store the initial position
        distanceScrolled = 0f; // Reset the scrolled distance
    }

    void Update()
    {
        float scrollAmount = scrollSpeed * Time.deltaTime;
        transform.Translate(Vector3.back * scrollAmount, Space.World);
        distanceScrolled += scrollAmount;

        if (distanceScrolled >= resetInterval)
        {
            transform.position = startPosition;
            distanceScrolled = 0f;
        }
    }

    // This method allows for runtime adjustment of scroll speed
    public void SetScrollSpeed(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }
}
