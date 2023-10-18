using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeaponIndex = 0; // Index of the currently selected weapon
    public List<Weapon> weapons; // List of available weapons
    public GunController gunController; // Reference to the gun controller

    // Start is called before the first frame update
    private void Start()
    {
        // Initialize the weapon selection
        SelectWeapon(selectedWeaponIndex);

        // If gunController wasn't set in the inspector, get it from the current GameObject
        if (gunController == null)
        {
            gunController = GetComponent<GunController>();
        }

        // You could also populate your 'weapons' list here if it's not set in the Inspector.
        // For example, finding them from the children of this GameObject or through any other logic you prefer.
    }

    // Update is called once per frame
    private void Update()
    {
        // Input handling for switching weapons
        int previousSelectedWeapon = selectedWeaponIndex;

        // Process input for switching weapons (e.g., scroll wheel, number keys, etc.)
        // This is just an example using the scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            selectedWeaponIndex++;
            if (selectedWeaponIndex >= weapons.Count) // If out of bounds, wrap to the beginning
                selectedWeaponIndex = 0;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            selectedWeaponIndex--;
            if (selectedWeaponIndex < 0) // If negative, wrap to the end
                selectedWeaponIndex = weapons.Count - 1;
        }

        // If the weapon selection has changed, select the new weapon
        if (previousSelectedWeapon != selectedWeaponIndex)
        {
            SelectWeapon(selectedWeaponIndex);
        }
    }

    private void SelectWeapon(int index)
    {
        // Simple logic to activate the selected weapon and deactivate others
        for (int i = 0; i < transform.childCount; i++)
        {
            // Assumes weapons are children of this GameObject and enabled/disabled as they are selected/deselected
            transform.GetChild(i).gameObject.SetActive(i == index);
        }

        // Update the current weapon in GunController
        if (weapons != null && weapons.Count > 0 && index < weapons.Count)
        {
            gunController.currentWeapon = weapons[index];
        }
        else
        {
            Debug.LogError("Weapon list is not set up correctly or no weapons are available.");
        }
    }
}
