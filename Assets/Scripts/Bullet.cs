using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Public variables: set these in the inspector or when instantiating the bullet.
    public float bulletForce; // The force applied when the bullet is shot.
    public float lifetime = 2f; // How long the bullet lives before automatic destruction.
    public float damage; // The damage the bullet will cause on impact.
    public AudioClip hitSound; // The sound effect played on impact.

    // Internal components.
    private Rigidbody2D rb;
    private AudioSource audioSource; // For playing the hitSound.

    void Start()
    {
        // Setup rigid body component.
        rb = GetComponent<Rigidbody2D>();

        // Setup audio source component.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // AudioSource component missing, adding one now.
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("No AudioSource found on the bullet prefab. One has been added automatically.");
        }

        // Configure the AudioSource properties.
        audioSource.playOnAwake = false;

        // If needed, you can add more configurations here (e.g., audioSource.volume, etc.).

        // Bullet self-destructs after its lifetime to avoid memory clutter.
        Destroy(gameObject, lifetime);
    }

    // Handle bullet impact.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Check if we hit something with a HealthManager (i.e., it can be damaged).
        HealthManager healthManager = hitInfo.GetComponent<HealthManager>();
        if (healthManager != null)
        {
            // We've hit something that has health: deal damage!
            healthManager.TakeDamage(damage);
        }

        // Play the hit sound, if one has been assigned.
        if (hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
            GetComponent<Collider2D>().enabled = false; // Prevent further collisions as the sound plays.

            // Keep the bullet alive long enough for the sound to play, then destroy it.
            Destroy(gameObject, hitSound.length);
        }
        else
        {
            // No hit sound was set: log an error for debugging.
            Debug.LogError("No hit sound was assigned to the bullet prefab. Instant destruction.");

            // No sound to play: destroy the bullet immediately.
            Destroy(gameObject);
        }
    }
}
