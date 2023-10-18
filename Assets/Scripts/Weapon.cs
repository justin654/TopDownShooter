using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : ScriptableObject
{
    public string name = "New Weapon";
    public Sprite icon = null; // If you want an icon for the weapon
    public float damage = 10f;
    public float range = 100f;
    public GameObject bulletPrefab; // Prefab for the bullet that this weapon shoots
    public float bulletForce = 20f; // Speed or force of the bullet
    // You can also add other attributes like fire rate, reload time, etc.
}
