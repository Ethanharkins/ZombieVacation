using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float chaseSpeed = 5f;
    public float circlingSpeed = 50f;
    public float circlingRadius = 5f;
    public float detectionRadius = 10f;
    public GameObject explosionEffectPrefab; // Assign your explosion effect prefab in the inspector

    private Transform player;
    private bool isCircling = false;
    private Vector3 circlingDirection;
    private bool previouslyCircling = false; // Track if the enemy was circling in the previous frame

    void Start()
    {
        // Automatically find the player by tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Using ?. for null-conditional operator
        if (player == null)
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

        // Determine if the enemy should start or stop circling
        bool shouldCircle = distanceToPlayer <= detectionRadius;
        if (shouldCircle != isCircling)
        {
            isCircling = shouldCircle;
            if (isCircling)
            {
                GameManager.Instance.RegisterEnemy();
            }
            else
            {
                GameManager.Instance.UnregisterEnemy();
            }
        }

        // Perform circling or chasing behavior based on isCircling flag
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
        // Unregister from GameManager if this enemy is destroyed for any reason
        GameManager.Instance?.UnregisterEnemy();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1); // Implement damage logic here
        }
    }

    public void TakeDamage(int damage)
    {
        // Placeholder for damage logic. Adjust as necessary.
        // Assuming you have a health variable that gets reduced by damage
        // health -= damage;

        // Check if health has fallen below 1, indicating the UFO should be destroyed
        if (/*health <= 0*/ true) // Replace this condition with your health check
        {
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject); // Destroy this enemy
        }
    }
}
