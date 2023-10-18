using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public float bulletForce; // The force will be set from the weapon when the bullet is instantiated

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Apply force to the bullet
        rb.AddForce(transform.up * bulletForce, ForceMode2D.Impulse);
    }

    // Optionally, handle collisions with targets, etc.
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // You can handle collisions here: deal damage, destroy the bullet, etc.
        // For example, if your targets have a script 'Target', you could do something like:
        // Target target = hitInfo.GetComponent<Target>();
        // if (target != null) target.TakeDamage(damage);

        Destroy(gameObject); // Destroy the bullet on hitting something
    }
}
