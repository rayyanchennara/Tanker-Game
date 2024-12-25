using System;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    float maxHitPoints = 10f;
    float currentHitPoints;
    float amount = 15f;

    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] ParticleSystem blastingParticle;

    [SerializeField] HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        smokeParticle.Stop();
        blastingParticle.Stop();
        currentHitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHitPoints()
    {
        currentHitPoints -= 2f;
        if(currentHitPoints == 0)
        {
            smokeParticle.Play();
            Destroy(gameObject,1.5f);
            healthBar.RestorHealth(amount);
        }
    }
}
