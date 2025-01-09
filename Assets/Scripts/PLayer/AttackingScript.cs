using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Variables")]
    float firingRange = 115f; // Range of the raycast
    int damageAmount = 250; // Amount of damage to deal

    [Header("Bools")]
    bool canFire = true; // Flag to check if the weapon can fire

    [Header("Game Objects")]
    [SerializeField] GameObject raySphere; // GameObject used as the origin of the raycast
    [SerializeField] GameObject audioSourceObject; // GameObject containing the AudioSource component
    [SerializeField] GameObject hitEffectsForBarrel; // Prefab for hit effects on barrels
    [SerializeField] GameObject hitEffects; // Prefab for hit effects on other objects

    [Header("Particles")]
    [SerializeField] ParticleSystem muzzleFlash; // Particle system for the muzzle flash

    [Header("AudioClips")]
    [SerializeField] AudioClip bombingSound; // AudioClip for the firing sound

    [Header("Texts")]
    [SerializeField] TextMeshProUGUI loadingText; // Text to display loading message

    [Header("References")]
    AudioSource audioSource; // Reference to the AudioSource component

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadingText.enabled = false; // Disable loading text at start
        audioSource = audioSourceObject.GetComponent<AudioSource>(); // Get the AudioSource component
    }

    // Update is called once per frame
    void Update()
    {
        FiringProcess(); // Call the firing process function
    }

    private void FiringProcess()
    {
        if (Input.GetButtonDown("Fire1") && canFire == true) // Check for fire input and if the weapon can fire
        {
            StartCoroutine(Bombing()); // Start the bombing coroutine
        }
    }

    IEnumerator Bombing()
    {
        canFire = false; // Set canFire to false to prevent firing during the coroutine
        PlayBombingSound(); // Play the bombing sound
        RayCastProcess(); // Perform the raycast
        PlayMussleFlash(); // Play the muzzle flash
        loadingText.enabled = true; // Enable the loading text
        yield return new WaitForSeconds(2f); // Wait for 2 seconds
        canFire = true; // Set canFire back to true
        loadingText.enabled = false; // Disable the loading text
    }

    private void PlayBombingSound()
    {
        audioSource.PlayOneShot(bombingSound); // Play the bombing sound once
    }

    private void PlayMussleFlash()
    {
        muzzleFlash.Play(); // Play the muzzle flash particle system
    }

    private void RayCastProcess()
    {
        RaycastHit hit; // Store the raycast hit information
        if (Physics.Raycast(raySphere.transform.position, raySphere.transform.forward, out hit, firingRange)) // Perform the raycast
        {
            EnemyHealthOne enemyHealthOne = hit.transform.GetComponent<EnemyHealthOne>(); // Get the EnemyHealthOne component
            EnemyHealthTwo enemyHealthTwo = hit.transform.GetComponent<EnemyHealthTwo>(); // Get the EnemyHealthTwo component
            EnemyHealthThree enemyHealthThree = hit.transform.GetComponent<EnemyHealthThree>(); // Get the EnemyHealthThree component
            BarrelScript barrelScript = hit.transform.GetComponent<BarrelScript>(); // Get the BarrelScript component
            if (enemyHealthOne != null) // Check if an EnemyHealthOne component was found
            {
                CreatHitEffects(hit); // Create hit effects
                enemyHealthOne.DecreaseHealth(damageAmount); // Decrease the enemy's health
            }

            else if (enemyHealthTwo != null) // Check if an EnemyHealthTwo component was found
            {
                CreatHitEffects(hit); // Create hit effects
                enemyHealthTwo.DecreaseHealth(damageAmount); // Decrease the enemy's health
            }

            else if (enemyHealthThree != null) // Check if an EnemyHealthThree component was found
            {
                CreatHitEffects(hit); // Create hit effects
                enemyHealthThree.DecreaseHealth(damageAmount); // Decrease the enemy's health
            }

            else if (barrelScript != null) // Check if a BarrelScript component was found
            {
                CreateHitEffectsOnBarrel(hit); // Create hit effects on the barrel
                barrelScript.DecreaseHitPoints(); // Decrease the barrel's hit points
            }
        }
    }

    private void CreateHitEffectsOnBarrel(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffectsForBarrel, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate the barrel hit effects
        Destroy(effect, 1); // Destroy the hit effects after 1 second
    }

    private void CreatHitEffects(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffects, hit.point, Quaternion.LookRotation(hit.normal)); // Instantiate the hit effects
        Destroy(effect, 1); // Destroy the hit effects after 1 second
    }
}
