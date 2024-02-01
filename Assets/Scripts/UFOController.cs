using UnityEngine;

public class UFOController : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float circlingSpeed = 50f;
    public float circlingRadius = 5f;
    public float detectionRadius = 10f;

    private bool isCircling = false;
    private Vector3 circlingDirection;
    private Transform gunTarget;

    void Start()
    {
        // Find the gun by tag
        GameObject gunObj = GameObject.FindGameObjectWithTag("Gun");
        if (gunObj != null)
        {
            gunTarget = gunObj.transform;
        }
        else
        {
            Debug.LogWarning("Gun object not found. Make sure your gun is tagged correctly.");
        }

        // Initial circling direction
        circlingDirection = transform.right;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

    }

    void Update()
    {
        if (gunTarget == null) return;

        float distanceToTarget = Vector3.Distance(transform.position, gunTarget.position);

        if (distanceToTarget <= detectionRadius && !isCircling)
        {
            // Start circling
            isCircling = true;
        }

        if (!isCircling)
        {
            // Chase the gun target
            Vector3 direction = (gunTarget.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
        else
        {
            // Calculate circling position
            circlingDirection = Quaternion.Euler(0, circlingSpeed * Time.deltaTime, 0) * circlingDirection;
            Vector3 targetPosition = gunTarget.position + circlingDirection * circlingRadius;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the UFO has collided with a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            DestroyUFO();
        }
    }

    void DestroyUFO()
    {
        // Optional: Add an explosion effect or sound here
        Destroy(gameObject);
    }
}
