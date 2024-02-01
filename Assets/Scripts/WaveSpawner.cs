using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of different enemy prefabs to spawn
    public Transform spawnPoint; // Central point for spawning enemies
    public float spawnInterval = 5.0f; // Time between each spawn
    private float timer;

    // Configuration for different spawning patterns
    public float lineSpacing = 3f;
    public int gridRows = 2;
    public int gridColumns = 3;
    public float gridSpacing = 2f;
    public int circleEnemyCount = 6;
    public float circleRadius = 5f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            // Example of cycling through different spawn patterns
            int wavePattern = Random.Range(0, 3); // Randomly choose a pattern
            switch (wavePattern)
            {
                case 0:
                    SpawnLineWave();
                    break;
                case 1:
                    SpawnGridWave();
                    break;
                case 2:
                    SpawnCircleWave();
                    break;
            }

            timer = 0f;
        }
    }

    void SpawnLineWave()
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            Vector3 spawnPos = spawnPoint.position + new Vector3(i * lineSpacing, 0, 0); // Line pattern
            Instantiate(enemyPrefabs[i % enemyPrefabs.Length], spawnPos, Quaternion.identity);
        }
    }

    void SpawnGridWave()
    {
        for (int row = 0; row < gridRows; row++)
        {
            for (int col = 0; col < gridColumns; col++)
            {
                Vector3 spawnPos = spawnPoint.position + new Vector3(col * gridSpacing, 0, row * gridSpacing); // Grid pattern
                Instantiate(enemyPrefabs[(row + col) % enemyPrefabs.Length], spawnPos, Quaternion.identity);
            }
        }
    }

    void SpawnCircleWave()
    {
        for (int i = 0; i < circleEnemyCount; i++)
        {
            float angle = i * Mathf.PI * 2 / circleEnemyCount;
            Vector3 spawnPos = new Vector3(Mathf.Cos(angle) * circleRadius, 0, Mathf.Sin(angle) * circleRadius);
            Instantiate(enemyPrefabs[i % enemyPrefabs.Length], spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }
}
