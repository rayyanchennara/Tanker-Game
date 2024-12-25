using System;
using System.Collections;
using UnityEngine;

public class AttackingScript : MonoBehaviour
{
    [SerializeField] GameObject raySphere;  
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] AudioClip bombingSound;
    [SerializeField] EnemyHealth enemyHealth;

    [SerializeField] GameObject hitEffects;
    float range = 100f;
    int amount = 2;

    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        FiringProcess();
    }

    private void FiringProcess()
    {
        if (Input.GetMouseButton(0))
        {
            StartCoroutine(Bombing());
        }
    }

    IEnumerator Bombing()
    {
        PlayBombingSound();
        RayCastProcess();
        PlayMussleFlash();
        yield return new WaitForSeconds(1);
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
            EnemyMover enemyMover = hit.transform.GetComponent<EnemyMover>();
            BarrelScript barrelScript = hit.transform.GetComponent<BarrelScript>();
            if(enemyMover != null)
            {
                CreatHitEffects(hit);
                enemyHealth.DecreaseHealth(amount);
            }

            else if(barrelScript != null)
            {
                barrelScript.DecreaseHitPoints();
            }
        }
    }

    private void CreatHitEffects(RaycastHit hit)
    {
        GameObject effect = Instantiate(hitEffects, hit.point,Quaternion.LookRotation(hit.normal));
        Destroy(effect,1);
    }
}
