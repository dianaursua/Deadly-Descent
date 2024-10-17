using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager instance;  // Singleton instance for global access
    public int totalCollectibles = 5;           // The number of collectibles in the scene
    public GameObject door;                     // Reference to the door that will open

    private int collectedCount = 0;             // Number of collected items

    private void Awake()
    {
        // Set up the singleton pattern to ensure there's only one instance of the manager
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectItem()
    {
        collectedCount++;  // Increment the count when a collectible is picked up

        if (collectedCount >= totalCollectibles)
        {
            OpenDoor();     // Open the door when enough items have been collected
        }
    }

    private void OpenDoor()
    {
        // You can add door-opening logic here (animation or deactivating the door)
        door.SetActive(false);  // Simple solution: deactivate the door to "open" it
    }
}
