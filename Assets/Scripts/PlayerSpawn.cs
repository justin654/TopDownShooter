using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab; // Assign your Player prefab in the inspector
    private GameObject playerInstance; // To hold the instance of the player

    // Start is called before the first frame update
    void Start()
    {
        // Find the spawn point in the scene
        GameObject spawnPoint = GameObject.Find("PlayerSpawn");

        // Check if the spawn point exists to avoid errors
        if (spawnPoint != null)
        {
            // Instantiate the player at the spawn point
            playerInstance = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
        else
        {
            Debug.LogError("No spawn point found in the scene. Please add a GameObject named 'PlayerSpawn'.");
        }
    }
}
