using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float fireRate = 2.0f;
    private float nextFireTime = 0f;
    public Camera mainCamera; // Assign the main camera

    void Update()
    {
        AimTowardsMouse();

        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0)) // Fire with left mouse button
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    void AimTowardsMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Vector3 direction = hitInfo.point - transform.position;
            direction.y = 0; // Keep the gun level, ignore vertical difference
            transform.forward = direction;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
    }
}
