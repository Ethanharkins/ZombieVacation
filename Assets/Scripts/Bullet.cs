using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Calculate the direction from the bullet's position to the cursor's position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 direction = (hit.point - transform.position).normalized;
            rb.velocity = direction * speed;
        }
        else
        {
            // If the raycast doesn't hit anything, default to moving forward
            rb.velocity = transform.forward * speed;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Optionally, update the score here or through a separate manager
            ScoreManager.Instance.IncreaseScore(1); // Assuming you have a method like this in your ScoreManager

            // Destroy the enemy
            Destroy(collision.gameObject);

            // Destroy the bullet itself
            Destroy(gameObject);
        }
    }
}


