using System;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    float maxHitPoints = 20f;
    float currentHitPoints;
    float amount = 15f;
    [SerializeField] AudioClip explosioAudio;

    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] ParticleSystem blastParticle;

    [SerializeField] HealthBar healthBar;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        smokeParticle.Stop();
        blastParticle.Stop();
        Mathf.Clamp(currentHitPoints, 0, maxHitPoints);
        currentHitPoints = maxHitPoints;
    }

    public void DecreaseHitPoints()
    {
        currentHitPoints -= 10f;
        if(currentHitPoints <= 0)
        {
            audioSource.PlayOneShot(explosioAudio);
            smokeParticle.Play();
            blastParticle.Play();
            Destroy(gameObject,1f);
            healthBar.RestorHealth(amount);
        }
    }
}
