using UnityEngine;

public class Axe : MonoBehaviour
{
    public int damage = 15; // Amount of damage the axe deals

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object hit has an EnemyHealth script

        EnemyHealth enemy = other.GetComponent<EnemyHealth>();

        // If the object has a health component, apply damage
        if (enemy != null)
        {
            enemy.TakeDamage(damage);  // Apply damage to the enemy
            // Optional: Add any visual or audio feedback for hitting the enemy
            Debug.Log("Axe hit: " + damage + " damage dealt.");
        }
    }
}
