using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject raySphere; 
    [SerializeField] GameObject projectilePrefab; 
    [SerializeField] GameObject audioSourceObject;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip bombingSound;
    [SerializeField] GameObject hitEffects;
    [SerializeField] GameObject hitEffectsForBarrel;
    [SerializeField] TextMeshProUGUI loadingText;
    float range = 115f;
    // float projectileSpeed = 100f;
    int amount = 250;

    bool isShoot = true;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        loadingText.enabled = false;
        audioSource = audioSourceObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FiringProcess();
    }

    private void FiringProcess()
    {
        if (Input.GetButtonDown("Fire1") && isShoot == true)
        {
            StartCoroutine(Bombing());
        }
    }

    IEnumerator Bombing()
    {
        isShoot = false;
        PlayBombingSound();
        RayCastProcess();
        PlayMussleFlash();
        loadingText.enabled = true;
        yield return new WaitForSeconds(2f);
        isShoot = true;
        loadingText.enabled = false;
    }

    private void PlayBombingSound()
    {
        audioSource.PlayOneShot(bombingSound);
    }

    private void PlayMussleFlash()
    {
        muzzleFlash.Play();
    }

    private void RayCastProcess()
    {
        RaycastHit hit;
        if(Physics.Raycast(raySphere.transform.position, raySphere.transform.forward, out hit, range))
        {
            EnemyHealthOne enemyHealthOne = hit.transform.GetComponent<EnemyHealthOne>();
            EnemyHealthTwo enemyHealthTwo = hit.transform.GetComponent<EnemyHealthTwo>();
            EnemyHealthThree enemyHealthThree = hit.transform.GetComponent<EnemyHealthThree>();
            BarrelScript barrelScript = hit.transform.GetComponent<BarrelScript>();
            if(enemyHealthOne != null)
            {
                CreatHitEffects(hit);
                enemyHealthOne.DecreaseHealth(amount);
            }

            else if(enemyHealthTwo != null)
            {
                CreatHitEffects(hit);
                enemyHealthTwo.DecreaseHealth(amount);
            }

            else if(enemyHealthThree != null)
            {
                CreatHitEffects(hit);
                enemyHealthThree.DecreaseHealth(amount);
            }

            else if(barrelScript != null)
            {
                CreateHitEffectsOnBarrel(hit);
                barrelScript.DecreaseHitPoints();
            }
        }
    }

    private void CreateHitEffectsOnBarrel(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffectsForBarrel, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect,1);
    }

    private void CreatHitEffects(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffects, hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(effect,1);
    }
}
