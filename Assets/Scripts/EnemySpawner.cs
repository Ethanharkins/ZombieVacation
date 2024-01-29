using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of different enemy prefabs to spawn
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
        if (enemyPrefabs.Length > 0)
        {
            // Randomly select an enemy prefab
            GameObject selectedPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            // Spawn the selected enemy prefab
            Instantiate(selectedPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No enemy prefabs assigned in the spawner.");
        }
    }
}
