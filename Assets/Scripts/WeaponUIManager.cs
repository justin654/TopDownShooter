using UnityEngine;
using TMPro; // Make sure you have this namespace for TextMeshPro access
using UnityEngine.UI; // Required for UI elements like Image


public class WeaponUIManager : MonoBehaviour
{
    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI healthText;
    public GunController playerGunController;
    public HealthManager playerHealthManager;
    public Image weaponIcon;


    // Start is called before the first frame update
    void Start()
    {
        if (weaponText == null)
        {
            Debug.LogError("Don't forget to assign the TextMeshPro component to the weaponText variable.");
            this.enabled = false; // Disable this script if the weaponText is not assigned
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
        UpdateWeaponUI(); // Updating weapon UI elements.
        UpdateHealthUI(); // Make sure to call this method in Update to continuously refresh health info.
    }

    void UpdateHealthUI()
    {
        if (playerHealthManager != null)
        {
            // Assuming there's a way to get current health from the HealthManager
            float currentHealth = playerHealthManager.CurrentHealth; // You might need to make a property or method in HealthManager to access this.
            healthText.text = "HP: " + currentHealth.ToString();
        }
        else
        {
            healthText.text = "HP: N/A"; // Show something default when health information isn't available.
        }
    }
    void UpdateWeaponUI()
    {
        // Check if the GunController and current weapon are available.
        if (playerGunController != null && playerGunController.currentWeapon != null)
        {
            weaponText.text = "Current Weapon: " + playerGunController.currentWeapon.name;
            weaponIcon.sprite = playerGunController.currentWeapon.icon;
            weaponIcon.enabled = true;
        }
        else
        {
            weaponText.text = "No Weapon Selected";
            weaponIcon.enabled = false; // If there's no weapon, we don't display an icon.
        }
    }
    // Called after the player is spawned to ensure the script has references to the player's components.
    public void SetPlayerGunController(GunController gunController)
    {
        playerGunController = gunController;
        // Assuming the HealthManager is on the same GameObject as the GunController,
        // you can get the HealthManager component like this:
        playerHealthManager = gunController.GetComponent<HealthManager>();
    }





}
