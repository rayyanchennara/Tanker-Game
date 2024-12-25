using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int currentHealth;
    int maxHealth = 100;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ScoreBord scoreBord;
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
        // UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if(currentHealth == 0)
        {
            explosionParticle.Play();
            Destroy(gameObject,1);
            scoreBord.IncreaseScore();
            gameOver.DecreaseEnemy();
        }
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;
        UpdateHealthUI();
    }

    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
    }
}
