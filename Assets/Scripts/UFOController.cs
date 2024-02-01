using UnityEngine;

public class UFOController : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 5f;
    public float circlingSpeed = 50f;
    public float circlingRadius = 5f;
    public float detectionRadius = 10f;

    private bool isCircling = false;
    private Vector3 circlingDirection;

    void Start()
    {
        if (player == null)
        {
            // Automatically find the player by tag
            GameObject playerObj = GameObject.FindGameObjectWithTag("Gun");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        // Initial circling direction
        circlingDirection = transform.right;
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius && !isCircling)
        {
            // Start circling
            isCircling = true;
        }

        if (!isCircling)
        {
            // Chase the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
        else
        {
            // Calculate circling position
            circlingDirection = Quaternion.Euler(0, circlingSpeed * Time.deltaTime, 0) * circlingDirection;
            Vector3 targetPosition = player.position + circlingDirection * circlingRadius;
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
