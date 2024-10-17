using UnityEngine;
using UnityEngine.SceneManagement; // Include this to manage scenes

public class TeleportDoor : MonoBehaviour
{
    private bool hasTeleported = false; // Flag to check if the player has already teleported

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTeleported)
        {
            hasTeleported = true; // Set the flag to true to prevent further teleportation in this level
            Debug.Log("Player entered the door.");
            GameManager.instance.AddPoints(3); // Award points
            Debug.Log("Points awarded. Total points: " + GameManager.instance.points);
            SceneManager.LoadScene("Prototype"); // Load the next scene
        }
    }

    // Optionally, you can reset the flag when leaving the door area
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasTeleported = false; // Reset the flag if the player exits the trigger area
        }
    }
}
