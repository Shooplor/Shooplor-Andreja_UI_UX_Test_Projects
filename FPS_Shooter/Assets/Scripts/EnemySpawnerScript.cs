using System.Collections;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform[] spawnPoints;

    [Header("Wave Settings")]
    [SerializeField] int wavesToSpawn = 5;
    [SerializeField] float waveCooldown = 5f;

    private int currentWave = 0;
    private int enemiesAlive = 0;
    private bool waitingForWaveCompletion = false;

    void Start()
    {
        StartCoroutine(WaveManager());
    }

    IEnumerator WaveManager()
    {
        while (currentWave < wavesToSpawn)
        {
            currentWave++;
            int enemiesInWave = 10 * currentWave; // 10, 20, 30, 40, 50

            Debug.Log($"STARTING WAVE {currentWave} - SPAWNING {enemiesInWave} ENEMIES");

            // Spawn all enemies for current wave
            for (int i = 0; i < enemiesInWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.3f); // Small delay between spawns
            }

            waitingForWaveCompletion = true;

            // Wait until all enemies are dead
            Debug.Log($"Waiting for all enemies to be defeated...");
            yield return new WaitUntil(() => enemiesAlive <= 0);

            // Pause before next wave
            if (currentWave < wavesToSpawn)
            {
                Debug.Log($"Wave {currentWave} cleared! Waiting {waveCooldown} seconds...");
                yield return new WaitForSeconds(waveCooldown);
            }
        }

        Debug.Log("ALL WAVES COMPLETED!");
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesAlive++;
    }

    // Call this from your enemy's death script
    public void OnEnemyDeath()
    {
        enemiesAlive--;
    }
}