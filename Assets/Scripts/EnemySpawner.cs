using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The enemy prefab to be spawned
    public GameObject[] spawnPoints; // Array of possible spawn points
    public float spawnInterval = 5f; // Time between each spawn
    public int maxEnemiesOnScreen; // Maximum number of enemies on screen at a time
    private int currentEnemyCount;

    // Start is called before the first frame update
    void Start()
    {
        // Optionally, find spawn points dynamically, e.g., if they're tagged "EnemySpawn"
        // spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");

        // Check if everything is set up correctly
        if (enemyPrefab == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Setup error: Ensure enemy prefab and spawn points are set in the spawner.");
            return;
        }

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Infinite loop, consider adding a condition to stop spawning (e.g., when the player dies)
        while (true)
        {
            // Check if we are under the limit of enemies on screen
            if (currentEnemyCount < maxEnemiesOnScreen)
            {
                SpawnEnemy();
            }

            // Wait for the defined interval before spawning again
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject selectedSpawnPoint = spawnPoints[randomIndex];

        // Instantiate an enemy at the selected point
        Instantiate(enemyPrefab, selectedSpawnPoint.transform.position, Quaternion.identity);

        // Increase count of current enemies
        currentEnemyCount++;
    }

    // Call this method from the enemy script when it is destroyed
    public void EnemyDestroyed()
    {
        if (currentEnemyCount > 0)
            currentEnemyCount--;
    }
}
