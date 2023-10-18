using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab; // Assign your Player prefab in the inspector
    private GameObject playerInstance; // To hold the instance of the player
    public WeaponUIManager weaponUIManager; // Assign in inspector or find it via code

    // Start is called before the first frame update
    void Start()
    {
        // Find the spawn point in the scene
        GameObject spawnPoint = GameObject.Find("PlayerSpawn");

        // Check if the spawn point exists to avoid errors
        if (spawnPoint != null)
        {
            // Instantiate the player at the spawn point
            playerInstance = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity); // No rotation needed for 2D top-down

            // Now the player is instantiated, get the GunController and set it for the UI manager
            if (playerInstance != null) // Safeguarding against the prefab not being assigned
            {
                GunController gunController = playerInstance.GetComponent<GunController>();
                if (gunController != null && weaponUIManager != null)
                {
                    weaponUIManager.SetPlayerGunController(gunController);
                }
                else if (weaponUIManager == null)
                {
                    Debug.LogError("WeaponUIManager is not set in the SpawnManager.");
                }
                else
                {
                    Debug.LogError("Player prefab does not have a GunController component.");
                }
            }
            else
            {
                Debug.LogError("Player Prefab is not assigned in the inspector.");
            }
        }
        else
        {
            Debug.LogError("No spawn point found in the scene. Please add a GameObject named 'PlayerSpawn'.");
        }
    }
}
