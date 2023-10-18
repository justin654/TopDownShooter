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

        if (spawnPoint != null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);

            // Now the player is instantiated, get the GunController and set it for the UI manager
            GunController gunController = playerInstance.GetComponent<GunController>();
            if (gunController != null && weaponUIManager != null)
            {
                weaponUIManager.SetPlayerGunController(gunController);
            }
        }


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
