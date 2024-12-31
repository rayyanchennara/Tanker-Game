using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;
    
    [SerializeField] GameObject audioSourceObject;
    [SerializeField] AudioClip explosionAudio;
    [SerializeField] ParticleSystem explosionParticle;

    [SerializeField] GameOver gameOver;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = audioSourceObject.GetComponent<AudioSource>();
        explosionParticle.Stop();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(explosionAudio);
            explosionParticle.Play();
            Destroy(gameObject);
            gameOver.GameOverWindow();
        }
    }
}
