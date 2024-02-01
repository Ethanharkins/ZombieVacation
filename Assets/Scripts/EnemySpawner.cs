using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of different enemy prefabs to spawn
    public Transform target; // Target that enemies should face towards when spawned
    public float spawnInterval = 5.0f; // Time between each spawn
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefabs.Length > 0 && target != null)
        {
            // Randomly select an enemy prefab
            GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Calculate the direction to the target
            Vector3 directionToTarget = target.position - transform.position;
            Quaternion desiredRotation = Quaternion.LookRotation(directionToTarget);

            // Spawn the selected enemy prefab facing the target
            Instantiate(selectedPrefab, transform.position, desiredRotation);
        }
        else
        {
            if (enemyPrefabs.Length == 0)
            {
                Debug.LogWarning("No enemy prefabs assigned in the spawner.");
            }
            if (target == null)
            {
                Debug.LogWarning("Target not assigned in the spawner.");
            }
        }
    }
}
