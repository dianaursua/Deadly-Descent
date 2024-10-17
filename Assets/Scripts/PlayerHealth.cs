using UnityEngine;
using UnityEngine.SceneManagement; // Include this for scene management

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;        // Maximum health of the player
    private int currentHealth;         // Current health of the player

    public HealthBar healthBar;        // Reference to the UI health bar

    void Start()
    {
        currentHealth = maxHealth;     // Initialize the player's health to max at the start
        healthBar.SetMaxHealth(maxHealth);  // Set the health bar to max
    }

    // Call this method to deal damage to the player
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;       // Reduce the player's health by the damage amount
        if (currentHealth < 0)
        {
            currentHealth = 0;         // Prevent health from going below zero
        }
        healthBar.SetHealth(currentHealth, maxHealth);  // Update the health bar

        Debug.Log("Player took " + damage + " damage, current health: " + currentHealth); // Log the damage and health

        if (currentHealth <= 0)
        {
            Die();                     // If health is 0 or less, call the Die method
        }
    }

    // Call this method to heal the player (optional)
    public void Heal(int healAmount)
    {
        currentHealth += healAmount;   // Increase the player's health by the heal amount
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth; // Make sure health doesn't exceed max health
        }
        healthBar.SetHealth(currentHealth, maxHealth);  // Update the health bar
    }

    // Called when the player's health reaches zero
    private void Die()
    {
        Debug.Log("Player Died!");     // Log the death event
        // Reload the current scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
