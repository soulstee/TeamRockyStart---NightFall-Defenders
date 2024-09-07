using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required for Image components

public class Tower_v2 : MonoBehaviour
{
    public float maxHealth = 100f;     // Maximum health of the tower
    private float currentHealth;       // Current health of the tower

    public Image greenHealthBar;       // The Green health bar Image
    public Color fullHealthColor = Color.green;  // Color when health is full (green)
    public Color zeroHealthColor = Color.red;    // Color when health is zero (red)

    private void Start()
    {
        currentHealth = maxHealth;     // Initialize the tower's health to maxHealth

        // Ensure the health bar is full at the start
        UpdateHealthBar();
    }

    // Method to take damage from enemies
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0

        // Update the health bar and color
        UpdateHealthBar();

        // Check if the tower's health has reached 0
        if (currentHealth <= 0)
        {
            DestroyTower(); // Destroy the tower
        }
    }

    // Method to update the health bar fill amount and color
    private void UpdateHealthBar()
    {
        float healthPercent = currentHealth / maxHealth; // Calculate the current health percentage

        // Update the fill amount of the Green health bar (0 = empty, 1 = full)
        greenHealthBar.fillAmount = healthPercent;

        // Gradually change the health bar's color from green to red based on health percentage
        greenHealthBar.color = Color.Lerp(zeroHealthColor, fullHealthColor, healthPercent);
    }

    // Method to handle the destruction of the tower
    private void DestroyTower()
    {
        // Play destruction effects, sounds, etc. (optional)
        Debug.Log("The tower has been destroyed!");

        // Optionally, trigger end of game or other effects
        GameManager.instance.EndGame(); // Example: end the game
    }
}

