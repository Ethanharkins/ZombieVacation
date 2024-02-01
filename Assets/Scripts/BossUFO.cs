using UnityEngine;

public class BossUFO : MonoBehaviour
{
    public int health = 5;
    public GameObject explosionEffect; // Assign an explosion prefab if available
    public Transform player;
    public float followDistance = 20f; // Distance to keep from the player

    void Update()
    {
        if (player != null)
        {
            // Keep the boss at a distance from the player but face the player
            Vector3 directionToPlayer = player.position - transform.position;
            if (directionToPlayer.magnitude > followDistance)
            {
                // Move towards the player if further than followDistance
                transform.position += directionToPlayer.normalized * Time.deltaTime;
            }
            transform.LookAt(player);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }
}
