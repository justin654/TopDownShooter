using UnityEngine;

public class GunController : MonoBehaviour
{
    public Weapon currentWeapon; // Reference to the equipped weapon. Assign in the inspector or through other methods in-game.
    public Transform firePoint; // The location where bullets will be spawned.

    private Camera mainCamera; // Cached reference to the main camera used for converting screen point to world point.

    void Start()
    {
        // Initialize references
        mainCamera = Camera.main; // Cache for performance reasons.
    }

    void Update()
    {
        // Listen for the player's input to shoot
        if (Input.GetButtonDown("Fire1")) // "Fire1" is typically the left mouse button.
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Check if a weapon is equipped
        if (currentWeapon == null)
        {
            Debug.LogError("Attempted to fire without a weapon equipped.");
            return; // Exit if there's no weapon selected
        }

        // Creating a bullet from the prefab
        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>(); // Accessing the Bullet script

        if (bulletScript == null)
        {
            Debug.LogError("Bullet prefab does not contain a Bullet script.");
            return; // Critical failure: The bullet prefab is improperly configured.
        }

        // Set properties on the bullet, specified by the weapon
        bulletScript.damage = currentWeapon.damage;

        // Try to get the Rigidbody2D from the bullet prefab
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Bullet prefab missing Rigidbody2D component.");
            return; // Critical failure: The bullet prefab is improperly configured.
        }

        // Calculate the shooting direction
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = firePoint.position.z; // Ensure the z-axis doesn't interfere with calculations

        Vector2 shootingDirection = (mouseWorldPosition - firePoint.position).normalized;

        // Apply shooting force
        rb.AddForce(shootingDirection * currentWeapon.bulletForce, ForceMode2D.Impulse);
    }
}
