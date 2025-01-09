using System;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Variables")]
    float health; // Current health of the object
    float lerpTimer; // Timer used for smooth health bar transition
    float maxHealth = 100f; // Maximum health the object can have
    float speed = 2f; // Speed of the health bar fill animation

    [Header("Images")]
    [SerializeField] Image frontHealthBar; // Reference to the front (colored) part of the health bar
    [SerializeField] Image backHealthBar;  // Reference to the back (background) part of the health bar

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth; // Set initial health to maximum health
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); // Clamp health value between 0 and maxHealth
        UpdateHealthUI(); // Call the function to update the health bar UI
    }

    public void UpdateHealthUI() // Function to update the health bar visuals
    {
        float frontFillAmount = frontHealthBar.fillAmount; // Store current fill amount of the front bar
        float backFillAmount = backHealthBar.fillAmount;   // Store current fill amount of the back bar
        float healthFraction = health / maxHealth;        // Calculate the health percentage (0 to 1)

        if (backFillAmount > healthFraction) // Check if the back bar needs to decrease
        {
            backHealthBar.color = Color.red; // Set back bar color to red (indicating damage)
            frontHealthBar.fillAmount = healthFraction; // Set front bar fill amount to current health percentage

            lerpTimer += Time.deltaTime; // Update the lerp timer
            float percentComplete = lerpTimer / speed; // Calculate the completion percentage for lerp
            percentComplete = percentComplete * percentComplete; // Apply easing for smoother animation
            backHealthBar.fillAmount = Mathf.Lerp(backFillAmount, healthFraction, percentComplete); // Lerp the back bar fill amount
        }

        if (frontFillAmount < healthFraction) // Check if the front bar needs to increase
        {
            backHealthBar.color = Color.green; // Set back bar color to green (indicating healing)
            backHealthBar.fillAmount = healthFraction; // Set back bar fill amount to current health percentage

            lerpTimer += Time.deltaTime; // Update the lerp timer
            float percentComplete = lerpTimer / speed; // Calculate the completion percentage for lerp
            percentComplete = percentComplete * percentComplete; // Apply easing for smoother animation
            frontHealthBar.fillAmount = Mathf.Lerp(frontFillAmount, backHealthBar.fillAmount, percentComplete); // Lerp the front bar fill amount
        }
    }

    public void TakeDamage(float damage) // Function to decrease health
    {
        health -= damage; // Subtract damage from current health
        lerpTimer = 0f;  // Reset the lerp timer for smooth health bar transition
    }

    public void RestoreHealth(float amount) // Function to increase health
    {
        health += amount; // Add amount to current health
        lerpTimer = 0f;  // Reset the lerp timer for smooth health bar transition
    }
}
