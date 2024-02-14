using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void Initialize(float speed)
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
        Destroy(gameObject, 5f); // Cleanup bullet after 5 seconds
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Make sure your enemy GameObjects have the "Enemy" tag
        {
            Destroy(collision.gameObject); // Destroy the enemy
            GameManager.Instance.IncreaseScore(1); // Increase score by 1 (adjust value as needed)
            Destroy(gameObject); // Destroy the bullet
        }
    }

}
