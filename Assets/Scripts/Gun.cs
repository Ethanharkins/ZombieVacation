using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    private BulletManager bulletManager;
    private float nextFireTime = 0f;

    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    void Update()
    {
        AimTowardsCursor();

        if (Input.GetMouseButtonDown(0) && Time.time > nextFireTime)
        {
            bulletManager.ShootBullet(bulletSpawnPoint);
            nextFireTime = Time.time + bulletManager.GetCurrentBulletConfig().shootingInterval;
        }

        if (Input.GetKeyDown(KeyCode.U)) // Press U to upgrade bullet
        {
            bulletManager.UpgradeBullet();
        }
    }

    void AimTowardsCursor()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }


}
