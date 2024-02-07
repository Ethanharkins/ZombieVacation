using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float chaseSpeed = 5f;
    public float circlingSpeed = 50f;
    public float circlingRadius = 5f;
    public float detectionRadius = 10f;
    public GameObject explosionEffectPrefab; // Assign your explosion effect prefab in the inspector

    private bool isCircling = false;
    private Vector3 circlingDirection;
    private bool previouslyCircling = false; // Track if the enemy was circling in the previous frame

    void Start()
    {
        // Automatically find the player by tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player"); // Make sure your player GameObject is tagged with "Player"
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure your player is tagged correctly.");
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
            if (!previouslyCircling)
            {
                GameManager.Instance.RegisterEnemy();
                previouslyCircling = true;
            }
        }
        else if (distanceToPlayer > detectionRadius && isCircling)
        {
            // Stop circling
            isCircling = false;
            if (previouslyCircling)
            {
                GameManager.Instance.UnregisterEnemy();
                previouslyCircling = false;
            }
        }

        if (isCircling)
        {
            // Calculate circling position
            circlingDirection = Quaternion.Euler(0, circlingSpeed * Time.deltaTime, 0) * circlingDirection;
            Vector3 targetPosition = player.position + circlingDirection * circlingRadius;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, chaseSpeed * Time.deltaTime);
        }
        else
        {
            // Chase the player
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * chaseSpeed * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        if (isCircling)
        {
            // Unregister from GameManager if this enemy is destroyed while circling
            GameManager.Instance.UnregisterEnemy();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1); // Assuming 1 bullet = 1 damage
        }
    }

    public void TakeDamage(int damage)
    {
        // Assuming you have a health variable that gets reduced by damage
        // health -= damage;

        // Check if health has fallen below 1, indicating the UFO should be destroyed
        if (/*health <= 0*/ true) // Placeholder condition
        {
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
