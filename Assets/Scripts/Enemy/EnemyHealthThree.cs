using UnityEngine;

public class EnemyHealthThree : MonoBehaviour
{
    [Header ("Variables")]
    int currentHealth; // Stores the current health points of enemy One
    int maxHealth = 100; // Defines the maximum health points

    [Header ("Audio Clips")]
    [SerializeField] AudioClip explosionSound; // Sound effect to play when this enemy will destroy

    [Header ("Particles")]
    [SerializeField] ParticleSystem explosionParticle; // Particle effect to play when this enemy will destroy

    [Header ("Scripts")]
    [SerializeField] ScoreBord scoreBord; // Reference to the ScoreBoard Script
    [SerializeField] GameOver gameOver; // Reference to the GameOver Script

    [Header ("Cash Reference")]
    AudioSource audioSource; // Stores a reference to the AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        explosionParticle.Stop(); // Stop the explosion particle effect intially
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Clamp the intial health between 0 and maxHealth
        currentHealth = maxHealth; // Set current health to max health at the start
    }

    // Methord to update health UI
    private void UpdateHealthUI()
    {
        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(explosionSound); // Play the explosion sound effect
            explosionParticle.Play(); // Play the explosion Particle effect
            scoreBord.IncreaseScore(); // For Increase score When this enemy will die
            gameOver.DecreaseEnemy(); // For Decrease Enemy count from total enemies
            Destroy(gameObject,1); // Destroy the GameObject after a one second delay
        }
    }

    // Function to decrease enemy health when taking damage
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount; // Subtract the damage amount from current health when player attacks enemy
        UpdateHealthUI(); // For update health Ui whenever enemy health will decrease
    }
}
