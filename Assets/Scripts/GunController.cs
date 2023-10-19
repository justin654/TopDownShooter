using UnityEngine;

public class GunController : MonoBehaviour
{
    public Weapon currentWeapon; // This is your current weapon, which you might switch out.
    public Transform firePoint; // The point from which bullets will be fired.

    private Camera mainCamera; // Main camera reference for screen-to-world calculations.

    private void Start()
    {
        mainCamera = Camera.main; // Obtain the main camera once at the start.
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Default fire button is the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("No weapon selected!");
            return;
        }

        // Instantiate bullet and retrieve the Bullet script
        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Set the bullet's damage.
            bulletScript.damage = currentWeapon.damage;
        }
        else
        {
            Debug.LogError("No Bullet script attached to the bullet prefab.");
            return;
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Get the mouse position in world coordinates
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; // Ensure it's a 2D vector

            // Calculate the shooting direction
            Vector2 shootDirection = (mouseWorldPosition - firePoint.position).normalized;

            // Apply force to the bullet's Rigidbody2D component in the shooting direction
            rb.AddForce(shootDirection * currentWeapon.bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("No Rigidbody2D component found on the bullet prefab.");
        }
    }
}
