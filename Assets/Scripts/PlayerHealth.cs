using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    int maxHealth = 100;
    int currentHealth;
    [SerializeField] ParticleSystem explosionParticle;

    [SerializeField] GameOver gameOver;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            explosionParticle.Play();
            Destroy(gameObject);
            gameOver.GameOverWindow();
        }
    }
}
