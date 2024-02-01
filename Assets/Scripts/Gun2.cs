using UnityEngine;

public class Gun2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 0.5f; // Player can shoot once every 0.5 seconds
    private float nextFireTime = 0f;
    public Camera mainCamera; // Assign the main camera

    void Update()
    {
        AimTowardsMouse();

        // Check if the current time is greater than the next allowed fire time
        if (Time.time > nextFireTime)
        {
            // Check if the player has pressed the fire button (left mouse button)
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
                // Update the next fire time based on the current time and fire rate
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void AimTowardsMouse()
    {
        // Create a ray from the camera through the mouse position
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // Perform a raycast to find where the ray intersects with a collider in the game world
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            // Calculate the direction from the gun to the hit point
            Vector3 direction = hitInfo.point - transform.position;
            // Adjust the direction to keep the gun level (ignore vertical difference)
            direction.y = 0;
            // Rotate the gun to face the calculated direction
            transform.forward = direction;
        }
    }

    void Shoot()
    {
        // Instantiate the bullet prefab at the bullet spawn point's position and rotation
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
