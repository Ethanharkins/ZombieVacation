using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    public void SpawnWave(int waveType)
    {
        switch (waveType)
        {
            case 1:
                SpawnArcWave();
                break;
            case 2:
                SpawnLineWave();
                break;
            case 3:
                SpawnRandomWave();
                break;
            default:
                Debug.LogError("Unknown wave type: " + waveType);
                break;
        }
    }

    private void SpawnArcWave()
    {
        int enemyCount = 4;
        float arcAngle = 45f; // Adjust as needed

        for (int i = 0; i < enemyCount; i++)
        {
            // Calculate position in an arc
            float angle = i * arcAngle / (enemyCount - 1) - arcAngle / 2;
            Vector3 spawnPos = Quaternion.Euler(0, angle, 0) * Vector3.forward * 5f; // Adjust distance as needed
            Instantiate(enemyPrefab, spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }

    private void SpawnLineWave()
    {
        int enemyCount = 5;
        float spacing = 2f; // Adjust as needed

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = new Vector3(i * spacing, 0, 0); // Horizontal line
            Instantiate(enemyPrefab, spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }

    private void SpawnRandomWave()
    {
        int enemyCount = Random.Range(3, 7); // Random number of enemies
        float radius = 5f; // Adjust as needed

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = Random.insideUnitCircle * radius;
            spawnPos.z = spawnPos.y;
            spawnPos.y = 0;
            Instantiate(enemyPrefab, spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }
}
