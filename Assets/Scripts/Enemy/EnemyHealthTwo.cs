using UnityEngine;

public class EnemyHealthTwo : MonoBehaviour
{
    int currentHealth;
    int maxHealth = 100;

    [SerializeField] AudioClip explosionSound;

    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ScoreBord scoreBord;
    [SerializeField] GameOver gameOver;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        explosionParticle.Stop();
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
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
        if(currentHealth <= 0)
        {
            audioSource.PlayOneShot(explosionSound);
            explosionParticle.Play();
            scoreBord.IncreaseScore();
            gameOver.DecreaseEnemy();
            Destroy(gameObject,1);
        }
    }
    
    public void DecreaseHealth(int amount)
    {
        currentHealth -= amount;
        UpdateHealthUI();
    }
}
