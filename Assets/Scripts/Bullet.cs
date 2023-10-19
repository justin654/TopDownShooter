using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletForce; // The force will be set from the weapon when the bullet is instantiated
    public float lifetime = 2f; // Bullet lifetime in seconds
    public float damage; // You will set this value from the weapon when the bullet is instantiated.
    public AudioClip hitSound; // The sound that will play on hitting a target. Assign in inspector.
    private AudioSource audioSource; // The AudioSource component.



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Apply force to the bullet
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null) // If no AudioSource is found
        {
            // Log error message and add an AudioSource component
            Debug.LogError("No AudioSource component found on the bullet prefab. Adding one.");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // (Optional) Configure the AudioSource component
        audioSource.playOnAwake = false; // Don't play automatically
        // ... (any other configuration you want)


        // Destroy the bullet after a set time.
        Destroy(gameObject, lifetime);
    }

    // Optionally, handle collisions with targets, etc.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        HealthManager healthManager = hitInfo.GetComponent<HealthManager>();

        if (healthManager != null) // means whatever we hit has a HealthManager and therefore has health
        {
            healthManager.TakeDamage(damage); // call the TakeDamage method of HealthManager
        }
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            // Delay the destruction of the bullet until after the sound finishes playing
            Destroy(gameObject, hitSound.length);
        }
        else
        {
            Debug.LogError("No hit sound assigned to the bullet prefab.");
            // If there's no sound, we can destroy the bullet immediately as before
            Destroy(gameObject);
        }
    }
}

