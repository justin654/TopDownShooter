using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth; // Initialize with max health.

        // Locate the EnemySpawner in the scene.
        spawner = FindObjectOfType<EnemySpawner>();
        if (spawner == null)
        {
            Debug.LogError("No EnemySpawner found in the scene.");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            Die(); // Implement this function to handle the character's death.
        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
        else
        {
            Debug.LogError("Spawner reference not set on HealthManager.");
        }


        // If the player dies, you might want to trigger a game over state.
        if (gameObject.CompareTag("Player"))
        {
            // Add logic for what happens if the player dies (e.g., restart level, show game over screen...)
        }
        else // For enemies, this usually means being destroyed or disabled.
        {
            spawner.EnemyDestroyed();
            Destroy(gameObject); // For simplicity, destroy the object. You might want to replace this with more complex behavior.
        }

    }
}
