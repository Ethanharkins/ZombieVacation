using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 10f; // Speed of the bullet
    public float destroyTime = 5f; // Time after which the bullet is automatically destroyed
    private Rigidbody rb; // Rigidbody component for physics

    void Start()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component

        // If there's a specific target, calculate direction and set velocity towards it
        GameObject targetObject = GameObject.FindGameObjectWithTag("Gun");
        if (targetObject != null)
        {
            Transform target = targetObject.transform;
            Vector3 moveDirection = (target.position - transform.position).normalized;
            rb.velocity = moveDirection * speed;
        }
        else
        {
            Debug.LogError("EnemyBullet: Target not found.");
        }

        // Destroy the bullet after 'destroyTime' seconds to prevent it from existing indefinitely
        Destroy(gameObject, destroyTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check for collision with the player
        if (other.CompareTag("Player"))
        {
            // Assuming the player has a component for managing lives or health
            CarLives playerLives = other.GetComponent<CarLives>();
            if (playerLives != null)
            {
                playerLives.TakeDamage(); // Decrement the player's lives or health
            }
            Destroy(gameObject); // Destroy the enemy bullet upon hitting the player
        }
    }

    // Optional: Destroy the bullet upon leaving the camera view for optimization
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
