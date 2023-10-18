using UnityEngine;
using TMPro; // Make sure you have this namespace for TextMeshPro access

public class WeaponUIManager : MonoBehaviour
{
    // Reference to your TextMeshPro component
    public TextMeshProUGUI weaponText;

    // Reference to your player (or the script/component that holds the current weapon info)
    public GunController playerGunController;

    // Start is called before the first frame update
    void Start()
    {
        if (weaponText == null)
        {
            Debug.LogError("Don't forget to assign the TextMeshPro component to the weaponText variable.");
            this.enabled = false; // Disable this script if the weaponText is not assigned
        }

        if (playerGunController == null)
        {
            Debug.LogError("Don't forget to assign the player's GunController to this script.");
            this.enabled = false; // Disable this script if the playerGunController is not assigned
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGunController.currentWeapon != null)
        {
            // Assuming the weapon has a 'name' field. Replace 'name' with the actual field if it's different
            weaponText.text = "Current Weapon: " + playerGunController.currentWeapon.name;
        }
        else
        {
            weaponText.text = "No Weapon Selected";
        }
    }
}
