using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;

    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log("Wave " + waveIndex + " Incoming");

        switch (waveIndex % 3)
        {
            case 0:
                SpawnArcWave();
                break;
            case 1:
                SpawnLineWave();
                break;
            case 2:
                SpawnRandomWave();
                break;
        }

        yield return new WaitForSeconds(timeBetweenWaves);
    }

    private void SpawnArcWave()
    {
        int enemyCount = 4;
        float arcAngle = 45f; // Adjust as needed

        for (int i = 0; i < enemyCount; i++)
        {
            float angle = i * arcAngle / (enemyCount - 1) - arcAngle / 2;
            Vector3 spawnPos = Quaternion.Euler(0, angle, 0) * Vector3.forward * 5f;
            Instantiate(enemyPrefab, spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }

    private void SpawnLineWave()
    {
        int enemyCount = 5;
        float spacing = 2f; // Adjust as needed

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos = new Vector3(i * spacing, 0, 0);
            Instantiate(enemyPrefab, spawnPoint.position + spawnPos, Quaternion.identity);
        }
    }

    private void SpawnRandomWave()
    {
        int enemyCount = Random.Range(3, 7);
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
