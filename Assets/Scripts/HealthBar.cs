using UnityEngine;
using UnityEngine.UI; // Include this for the Image component
using TMPro; // Include this for TextMeshPro

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; // Reference to the Image component
    public TMP_Text healthText; // Reference to the TextMeshPro component for displaying health

    // Set maximum health
    public void SetMaxHealth(int health)
    {
        healthBarImage.fillAmount = 1f; // Start with the health bar full
        UpdateHealthText(health, health); // Display initial health
    }

    // Set current health
    public void SetHealth(int currentHealth, int maxHealth)
    {
        healthBarImage.fillAmount = (float)currentHealth / maxHealth; // Update fill amount
        UpdateHealthText(currentHealth, maxHealth); // Update health text
    }

    // Update health text display
    private void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + " / " + maxHealth; // Display current and max health
    }
}
