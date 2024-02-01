using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public GameObject enemyBulletPrefab;
    public float bulletSpeed = 10f;
    public Transform player;
    public float shootingInterval = 2f; // Time between shots
    private float shootingTimer;

    void Update()
    {
        if (player == null) return;

        shootingTimer += Time.deltaTime;
        if (shootingTimer >= shootingInterval)
        {
            ShootAtPlayer();
            shootingTimer = 0f;
        }
    }

    void ShootAtPlayer()
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
}
