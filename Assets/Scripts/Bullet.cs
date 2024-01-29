using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet hits an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy
            Destroy(collision.gameObject);
            // Destroy the bullet itself
            Destroy(gameObject);
        }
    }
}


