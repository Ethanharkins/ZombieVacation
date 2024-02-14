using UnityEngine;

[System.Serializable]
public class BulletConfig
{
    public GameObject bulletPrefab;
    public float shootingInterval;
    public float speed;
    public int numberOfBullets; // Used for upgrades that shoot multiple bullets
    public bool isSpray; // Determines if the bullet type uses a spray pattern
}

public class BulletManager : MonoBehaviour
{
    public BulletConfig[] bulletConfigs = new BulletConfig[3]; // Define three bullet types
    private int currentBulletIndex = 0;

    public void UpgradeBullet()
    {
        currentBulletIndex = Mathf.Min(currentBulletIndex + 1, bulletConfigs.Length - 1);
    }

    public BulletConfig GetCurrentBulletConfig()
    {
        return bulletConfigs[currentBulletIndex];
    }

    // This method will be called from the Gun script
    public void ShootBullet(Transform bulletSpawnPoint)
    {
        BulletConfig config = GetCurrentBulletConfig();
        Debug.Log($"Shooting bullet of type: {currentBulletIndex}, Prefab: {config.bulletPrefab.name}");
        if (config.isSpray)
        {
            // Implement spray shooting logic
            for (int i = 0; i < config.numberOfBullets; i++)
            {
                // Offset the instantiation point for spray pattern
                Vector3 offset = new Vector3((i - 1) * 0.5f, 0, 0); // Example offset for spray
                GameObject bullet = Instantiate(config.bulletPrefab, bulletSpawnPoint.position + offset, bulletSpawnPoint.rotation);
                bullet.GetComponent<Bullet>().Initialize(config.speed);
            }
        }
        else
        {
            GameObject bullet = Instantiate(config.bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Bullet>().Initialize(config.speed);
        }
    }
}
