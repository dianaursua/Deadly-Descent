using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public PlayerAttackController.WeaponType weaponType; // Reference to the weapon type
    private bool isPlayerInRange = false; // Check if player is in range

    private void Update()
    {
        // Check for player input to pick up the weapon
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E)) // Change KeyCode.E to your desired pick-up key
        {
            // Notify the player to equip the weapon
            FindObjectOfType<PlayerAttackController>().EquipWeapon(weaponType);
            Destroy(gameObject); // Destroy the pickup after it is collected
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Player is in range to pick up the weapon
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Player is out of range
        }
    }
}
