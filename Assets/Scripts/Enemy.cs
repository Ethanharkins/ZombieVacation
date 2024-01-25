using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float attackRange = 1.0f; // Distance at which the enemy starts attacking
    public float detectionRange = 5.0f; // Distance at which the enemy starts running towards the player
    public GameObject player; // Assign the player object in the inspector
    private Animator animator;
    private bool isAttacking = false;
    private float attackTimer = 3.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking)
            {
                // Start attacking
                isAttacking = true;
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsAttacking", true);
            }

            // Handle attack timing
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                Attack();
                attackTimer = 3.0f; // Reset attack timer
            }
        }
        else if (distanceToPlayer <= detectionRange)
        {
            // Run towards the player
            isAttacking = false;
            animator.SetBool("IsIdle", false);
            animator.SetBool("IsRunning", true);
            MoveTowardsPlayer();
        }
        else
        {
            // Idle
            isAttacking = false;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsIdle", true);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void Attack()
    {
        // Implement attack logic here
        // For example, dealing damage to the player
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Destroy both bullet and enemy
            Destroy(collision.gameObject);
            animator.SetBool("IsDead", true);
            // Optionally add a delay before destroying the enemy
            Destroy(gameObject, 1.0f); // Destroy after 1 second
        }
    }
}
