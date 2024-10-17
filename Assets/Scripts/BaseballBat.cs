using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    public int damage = 12; // Amount of damage the baseball bat deals
    public float attackCooldown = 1f; // Cooldown time in seconds
    private float lastAttackTime = 0f; // Track the last time the bat was used

    private void OnTriggerEnter(Collider other)
    {
        // Check if enough time has passed since the last attack
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            // Check if the object hit has an EnemyHealth script
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();

            // If the object has a health component, apply damage
            if (enemy != null)
            {
                enemy.TakeDamage(damage);  // Apply damage to the enemy
                lastAttackTime = Time.time; // Update the last attack time
                // Optional: Add any visual or audio feedback for hitting the enemy
                Debug.Log("Baseball Bat hit: " + damage + " damage dealt.");
            }
        }
    }
}
