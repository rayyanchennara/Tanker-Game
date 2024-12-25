using System;
using System.Xml.Serialization;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    float health;
    float lerpTimer;
    float maxHealth = 100;
    float speed = 2f;

    [SerializeField] Image forntHealthBar;
    [SerializeField] Image BackHealthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        float fillF = forntHealthBar.fillAmount;
        float fillB = BackHealthBar.fillAmount;
        float hFraction = health / maxHealth;

        if(fillB > hFraction)
        {
            Debug.Log(health);
            BackHealthBar.color = Color.red;
            forntHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / speed;
            percentComplete = percentComplete * percentComplete;
            BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if(fillF < hFraction)
        {
            BackHealthBar.color = Color.green;
            BackHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / speed;
            percentComplete = percentComplete * percentComplete;
            forntHealthBar.fillAmount = Mathf.Lerp(fillF, BackHealthBar.fillAmount, percentComplete);
        }
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestorHealth(float amount)
    {
        health += amount;
        lerpTimer = 0f;
    }
}
