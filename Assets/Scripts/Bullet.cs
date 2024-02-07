using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;

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
        Destroy(gameObject, 5f); // Destroys the bullet after 5 seconds to clean up
    }

    void OnCollisionEnter(Collision collision)
    {
        // Adjusted to check for "Enemy" and "BossUFO" tags for broader compatibility
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("BossUFO"))
        {
            Debug.Log("Bullet hit an enemy");
            GameManager.Instance.IncreaseScore(1); // Increase score by 1 using GameManager
            Destroy(collision.gameObject); // Destroy the enemy
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
