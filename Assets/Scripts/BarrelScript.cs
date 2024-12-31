using System;
using UnityEngine;

public class BarrelScript : MonoBehaviour
{
    float maxHitPoints = 20f;
    float currentHitPoints;
    float amount = 15f;

    [SerializeField] ParticleSystem smokeParticle;
    [SerializeField] ParticleSystem blastParticle;

    [SerializeField] HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        smokeParticle.Stop();
        blastParticle.Stop();
        Mathf.Clamp(currentHitPoints, 0, maxHitPoints);
        currentHitPoints = maxHitPoints;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DecreaseHitPoints()
    {
        currentHitPoints -= 10f;
        if(currentHitPoints <= 0)
        {
            smokeParticle.Play();
            blastParticle.Play();
            Destroy(gameObject,1f);
            healthBar.RestorHealth(amount);
        }
    }
}
