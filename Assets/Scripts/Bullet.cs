using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 5f; // Lifetime of the bullet in seconds

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = transform.forward * speed;
        }

        Debug.Log("Bullet fired towards " + rb.velocity);

        // Destroy the bullet after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet collided with " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit an enemy");

            // Optionally, increase the score here if you have a ScoreManager
            // ScoreManager.Instance.IncreaseScore(1);

            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Immediately destroy the bullet upon collision
        }
    }
}
