using System;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    [Header("Variables")]
    float maxHitPoints = 20f; // Maximum health points the object can have
    float currentHitPoints; // Current health points of the object
    float amount = 15f; // Amount used for health restoration (assumed)

    [Header("AudioClips")]
    [SerializeField] AudioClip explosionAudio; // Sound played when the object is destroyed

    [Header("Particles")]
    [SerializeField] ParticleSystem smokeParticle; // Particle system for smoke effect
    [SerializeField] ParticleSystem blastParticle; // Particle system for explosion effect

    [Header("Scripts")]
    [SerializeField] HealthBar healthBar; // Reference to the HealthBar script

    [Header("Reference")]
    AudioSource audioSource; // Reference to the AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        smokeParticle.Stop(); // Ensure smoke particle effect is stopped initially
        blastParticle.Stop(); // Ensure blast particle effect is stopped initially
        currentHitPoints = Mathf.Clamp(currentHitPoints, 0, maxHitPoints); // Clamp initial health points between 0 and max
    }

    public void DecreaseHitPoints() // Function to decrease health points
    {
        currentHitPoints -= 10f; // Reduce current health points by 10

        if (currentHitPoints <= 0) // Check if the object is destroyed (current health is 0 or less)
        {
            audioSource.PlayOneShot(explosionAudio); // Play the explosion sound
            smokeParticle.Play(); // Play the smoke particle effect
            blastParticle.Play(); // Play the blast particle effect
            Destroy(gameObject, 1f); // Destroy the object itself after 1 second
            healthBar.RestoreHealth(amount); // Call the HealthBar script's function to restore health (assumed functionality)
        }
    }
}
