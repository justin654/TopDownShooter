using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletForce; // The force will be set from the weapon when the bullet is instantiated
    public float lifetime = 2f; // Bullet lifetime in seconds
    public float damage; // You will set this value from the weapon when the bullet is instantiated.


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Apply force to the bullet
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);

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
        Destroy(gameObject); // consider object pooling if you have performance issues
    }
}

