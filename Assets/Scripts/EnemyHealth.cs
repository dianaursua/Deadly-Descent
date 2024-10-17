using UnityEngine;
using UnityEngine.UI;               // Import UI for handling health bar
using TMPro;                       // Import TextMeshPro namespace for handling text

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;          // Maximum health
    public int currentHealth;            // Current health
    public Image healthBarFill;          // Reference to the UI health bar's fill object (drag the Image from the Inspector)
    public TextMeshProUGUI healthText;   // Reference to the TextMeshProUGUI for displaying health text

    void Start()
    {
        currentHealth = maxHealth;       // Initialize health
        UpdateHealthBar();               // Update the health bar and text at the start
    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;         // Reduce health
        UpdateHealthBar();               // Update the health bar and text after taking damage

        if (currentHealth <= 0)
        {
            Die();                       // Call die if health reaches 0
        }
    }

    // Method to update the health bar and text
    void UpdateHealthBar()
    {
        // Ensure we are not dividing by zero and that healthBarFill is assigned
        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = (float)currentHealth / maxHealth;  // Update the fill amount based on health
        }

        // Update the health text if it's assigned
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString() + " / " + maxHealth.ToString();  // Show health in "current/max" format
        }
    }

    // Method for enemy death
    void Die()
    {
        Debug.Log("Enemy died!");        // Placeholder for death logic
        Destroy(gameObject);             // Destroy the enemy object
    }
}
