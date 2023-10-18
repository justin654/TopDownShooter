using UnityEngine;

public class GunController : MonoBehaviour
{
    public Weapon currentWeapon; // This is your current weapon, which you might switch out.
    public Transform firePoint; // The point from which bullets will be fired.

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

        Debug.Log("Shooting with weapon: " + currentWeapon.name); // Check if this method gets called

        // Instantiate bullet and retrieve the Bullet script
        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            // Set the bullet's damage.
            bulletScript.damage = currentWeapon.damage;

            // This assumes the Bullet script takes care of applying the force. If not, you might need the Rigidbody2D part here too.
        }
        else
        {
            Debug.LogError("No Bullet script attached to the bullet prefab.");
            return;
        }

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Apply force to the bullet's Rigidbody2D component.
            rb.AddForce(firePoint.up * currentWeapon.bulletForce, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("No Rigidbody2D component found on the bullet prefab.");
        }
    }
}
