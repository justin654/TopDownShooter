using UnityEngine;
using TMPro; // Make sure you have this namespace for TextMeshPro access
using UnityEngine.UI; // Required for UI elements like Image


public class WeaponUIManager : MonoBehaviour
{
    // Reference to your TextMeshPro component
    public TextMeshProUGUI weaponText;

    // Reference to your player (or the script/component that holds the current weapon info)
    public GunController playerGunController;

    public Image weaponIcon;

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

        if (weaponIcon == null)
        {
            Debug.LogError("Don't forget to assign the Image component to the weaponIcon variable.");
            this.enabled = false; // Disable this script if the weaponIcon is not assigned
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (playerGunController.currentWeapon != null)
        {
            // Update the text
            weaponText.text = "Current Weapon: " + playerGunController.currentWeapon.name;

            // Update the icon
            weaponIcon.sprite = playerGunController.currentWeapon.icon; // This assigns the weapon's icon to the Image component
            weaponIcon.enabled = true; // Ensure the icon is visible
        }
        else
        {
            weaponText.text = "No Weapon Selected";
            weaponIcon.enabled = false; // Hide the icon if there's no weapon
        }
    }

}
