using UnityEngine;

public class BossUFO : MonoBehaviour
{
    public int health = 5;
    public GameObject explosionEffectPrefab; // Ensure this is assigned in the Unity Editor
    public GameObject enemyBulletPrefab; // The prefab for the bullet shot by the Boss UFO
    public Transform shootingPoint; // The point from which the Boss UFO shoots
    public Transform target; // The target the Boss UFO aims at, e.g., the player's "Gun"
    public float followDistance = 10f; // Adjusted for closer approach
    public float chaseSpeed = 5f; // The speed at which the boss moves towards the target
    public float shootingInterval = 2f; // The interval between shots
    private float shootingTimer;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Gun").transform;
        shootingTimer = 0; // Initialize the shooting timer
    }

    void Update()
    {
        if (target == null) return;

        // Movement towards the target
        Vector3 directionToTarget = target.position - transform.position;
        float distance = directionToTarget.magnitude;
        if (distance > followDistance)
        {
            transform.position += directionToTarget.normalized * chaseSpeed * Time.deltaTime;
        }
        transform.LookAt(target);

        // Shooting logic
        shootingTimer -= Time.deltaTime;
        if (shootingTimer <= 0)
        {
            ShootAtTarget();
            shootingTimer = shootingInterval;
        }
    }

    void ShootAtTarget()
    {
        if (enemyBulletPrefab && shootingPoint)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, shootingPoint.position, Quaternion.identity);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Ensure the bullet moves towards the target
                rb.velocity = (target.position - shootingPoint.position).normalized * 10f; // Adjust bullet speed as necessary
            }
            else
            {
                Debug.LogError("EnemyBullet prefab lacks Rigidbody component.");
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (explosionEffectPrefab != null)
            {
                Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity); // Correctly instantiate the explosion effect
            }
            else
            {
                Debug.LogError("Explosion effect prefab not assigned.");
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(1); // Assuming each bullet does 1 damage
        }
    }
}
