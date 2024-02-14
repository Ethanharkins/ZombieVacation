using UnityEngine;

[System.Serializable]
public class BulletConfig
{
    public GameObject bulletPrefab; // The prefab for the bullet
    public float shootingInterval; // Time between shots
    public int numberOfBullets; // Number of bullets fired at once (e.g., for shotgun effects)
    public int cost; // Cost to upgrade to this bullet type
}

public class BulletManager : MonoBehaviour
{
    public BulletConfig[] bulletConfigs; // Configure this array in the Unity Inspector
    private int currentBulletIndex = 0; // Tracks the current bullet type
    private float lastShotTime; // Time since the last shot was fired

    void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        // This is a simple shooting mechanism; replace with your own input handling
        if (Input.GetButtonDown("Fire1") && Time.time - lastShotTime >= GetCurrentBulletConfig().shootingInterval)
        {
            ShootCurrentBullet();
            lastShotTime = Time.time;
        }
    }

    public void ShootCurrentBullet()
    {
        BulletConfig config = GetCurrentBulletConfig();

        // Example of shooting multiple bullets (e.g., shotgun spread)
        for (int i = 0; i < config.numberOfBullets; i++)
        {
            // Instantiate bullet at the position and rotation of the BulletManager (or gun)
            Instantiate(config.bulletPrefab, transform.position, transform.rotation);
            // Implement additional logic here for handling direction, spread, etc.
        }
    }

    public BulletConfig GetCurrentBulletConfig()
    {
        // Returns the current bullet configuration
        return bulletConfigs[currentBulletIndex];
    }

    public void UpgradeBullet()
    {
        // Ensure we don't exceed the array bounds and that we have enough score (handled elsewhere)
        if (currentBulletIndex < bulletConfigs.Length - 1)
        {
            // The actual score deduction and upgrade validation should be handled in your GameManager or equivalent
            // Here, we simply advance to the next bullet configuration
            currentBulletIndex++;
            Debug.Log("Upgraded to bullet type: " + (currentBulletIndex + 1));
        }
        else
        {
            Debug.Log("Maximum bullet upgrade reached.");
        }
    }
}
