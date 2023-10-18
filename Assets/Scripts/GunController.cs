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

        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("No Rigidbody2D component found on the bullet prefab.");
            return;
        }

        // Let's log the force being applied to confirm it's a sensible value
        Debug.Log("Applying force: " + (firePoint.up * currentWeapon.bulletForce));

        rb.AddForce(firePoint.up * currentWeapon.bulletForce, ForceMode2D.Impulse);
    }
}
