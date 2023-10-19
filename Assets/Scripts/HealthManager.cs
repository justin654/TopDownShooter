using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public GameObject hpBarPrefab; // Assign your HPBar prefab here through the Inspector
    public Transform uiCanvas; // Assign the main UI canvas of your scene here through the Inspector
    public Vector3 hpBarOffset = new Vector3(0, 1, 0); // Adjusts the position of the health bar above the entity.
    private GameObject hpBar; // The actual instantiated health bar in the scene
    private Image healthFill; // The UI element representing the current health level

    public EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;

        // Get the Canvas Transform from the CanvasManager
        uiCanvas = CanvasManager.Instance.CanvasTransform;

        // Instantiate and initialize the health bar as before
        hpBar = Instantiate(hpBarPrefab, uiCanvas);
        healthFill = hpBar.transform.Find("HealthFill").GetComponent<Image>();

        // Locate the EnemySpawner in the scene
        spawner = FindObjectOfType<EnemySpawner>();
        if (spawner == null)
        {
            Debug.LogError("No EnemySpawner found in the scene.");
        }

        UpdateHealthBar(); // Initial update to match the current health status
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
    }


    void Update()
    {
        // Make the health bar follow the enemy. Adjust the 'hpBarOffset' as necessary to match your desired position.
        if (hpBar != null)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position + hpBarOffset);
            hpBar.transform.position = screenPosition;
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        Debug.Log("Health after damage: " + currentHealth); // Logs can help verify actual health value.
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFill != null)
        {
            healthFill.fillAmount = currentHealth / maxHealth;
            Debug.Log("Health Fill: " + healthFill.fillAmount); // This log helps check the fill value.
        }
        else
        {
            Debug.LogWarning("healthFill is not set in the inspector");
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

        Destroy(hpBar); // Ensure the health bar also gets destroyed.

        // Handle player or enemy death here.
        if (gameObject.CompareTag("Player"))
        {
            // Handle player death (game over, reload, etc.)
        }
        else // Assuming it's an enemy
        {
            spawner.EnemyDestroyed(); // Notify the spawner
            Destroy(gameObject); // Destroy the enemy object
        }
    }
}
