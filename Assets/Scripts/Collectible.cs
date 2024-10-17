using UnityEngine;

public class Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the colliding object has the "Player" tag
        {
            Debug.Log("Player collected an item!"); // Debug message
            CollectibleManager.instance.CollectItem(); // Call the manager to collect the item
            Destroy(gameObject); // Destroy the collectible
        }
    }
}
