using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Variables")]
    int maxHealth = 100;  // Maximum health the object can have
    int currentHealth; // Current health of the object

    [Header("Game Objects")]
    [SerializeField] GameObject audioSourceObject; // Reference to the GameObject containing the AudioSource component

    [Header("AudioClips")]
    [SerializeField] AudioClip explosionAudio; // Reference to the AudioClip for the explosion sound

    [Header("Particles")]
    [SerializeField] ParticleSystem explosionParticle; // Reference to the ParticleSystem for the explosion effect

    [Header("Canvas")]
    [SerializeField] GameOver gameOver; // Reference to the GameOver script

    // Internal variables
    AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        audioSource = audioSourceObject.GetComponent<AudioSource>(); // Get the AudioSource component
        explosionParticle.Stop();  // Ensure the explosion particle effect is stopped initially
        currentHealth = maxHealth;  // Set current health to maximum health
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp current health between 0 and maxHealth
    }

    // Function to decrease health by a certain amount
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount; // Reduce current health by the specified amount
        UpdateHealthUI(); // Update the health UI (if applicable)
    }

    // Function to increase health by a certain amount
    public void IncreaseHealth(int amount)
    {
        currentHealth += amount; // Increase current health by the specified amount
        UpdateHealthUI(); // Update the health UI (if applicable)
    }

    // Function to update the health UI based on current health
    private void UpdateHealthUI()
    {
        if (currentHealth <= 0) // Check if the object is dead (current health is 0 or less)
        {
            audioSource.PlayOneShot(explosionAudio); // Play the explosion sound
            explosionParticle.Play(); // Play the explosion particle effect
            Destroy(gameObject); // Destroy the object itself
            gameOver.GameOverWindow(); // Call the GameOver script's function to display the game over UI
        }
    }
}
