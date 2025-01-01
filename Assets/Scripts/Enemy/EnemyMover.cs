using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [Header("Variables")]
    float chasingDistance = 250f; // Distance at which the enemy starts chasing the Player
    float distanceToTarget = Mathf.Infinity; // Distance between enemy and player
    float distanceToWayPoint = Mathf.Infinity; // Distance between enemy and next waypoint
    float rotationSpeed = 1f; // Speed which enemy using for rotation
    float range = 120f; // Range of the enemy's attack
    int amount = 2; // Damage dealt by the enemy's attack
    int wayPointNum = 0; // Index of the current waypoint in the patrol path

    [Header("Lists")]
    List<GameObject> wayPoints = new List<GameObject>(); // List of waypoints for patrolling

    [Header("Game Objects")]
    [SerializeField] GameObject parentOfWayPoints; // Parent object containing all waypoints
    [SerializeField] Transform target; // Transform of the Player
    [SerializeField] Transform raySphere; // Transform used for raycast attack

    [Header("ParticlSystem")]
    [SerializeField] ParticleSystem muzzleFlash; // Particle system for the muzzle flash effect

    [Header("AudioClips")]
    [SerializeField] AudioClip fireSound; // Audio Clip for Fire Sound

    [Header("Bools")]
    bool isProvoked = false; // For Enemy chase the player
    bool isPatrolling; // For Enemy patrol
    bool canFire = true; // Whether th enemy can Attack

    [Header("Cash Reference")]
    NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent Component
    AudioSource audioSource; // Reference to the AudioSource Component

    [Header("Scripts")]
    [SerializeField] PlayerHealth playerHealth; // Reference to PlayerHealth Script
    [SerializeField] HealthBar healthBar; // Reference to HealthBar Script
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get reference to AudioSource Component and NavMeshAgent Component
        audioSource = GetComponent<AudioSource>(); 
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Loop through child objects of the waypoints parent and add them to the waypoints list
        foreach (Transform child in parentOfWayPoints.transform)
        {
            wayPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate distance to current waypoint and player
        distanceToWayPoint = Vector3.Distance(transform.position, wayPoints[wayPointNum].transform.position);
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        ProcessToFollowTarget(); // Process if in patrol state to follow waypoint otherwise follow player
    }

    private void ProcessToFollowTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position); // Calculate distance to player
        // If chasing distance Value more than distace to player patrolling state will off and will chase player
        if (distanceToTarget <= chasingDistance)
        {
            isPatrolling = false;
            isProvoked = true;
        }
        // If distance to player more than chasing distance value patrol state will activate
        else if (distanceToTarget >= chasingDistance)
        {
            isProvoked = false;
            isPatrolling = true;
        }

        // If Chase Player state is activated EngageWithPlayer function will excecute
        if (isProvoked == true)
        {
            EngageWithPlayer();
        }

        // If Patrolling State is activated ProcessPatrolling Function will excecute
        if (isPatrolling == true)
        {
            ProcessPatrolling();
        }
    }

    private void ProcessPatrolling()
    {
        faceToWayPoint(); // Fuction to Rotate the enemy towards the current waypoint
        navMeshAgent.SetDestination(wayPoints[wayPointNum].transform.position); // Set th NavMeshAgent destination to the current waypoint

        // Check whether distance to way point less than 80
        if (distanceToWayPoint <= 80)
        {
            // If wayPoint number is less than last intex of list tha called waypoints 
            if (wayPointNum < wayPoints.Count - 1)
            {
                wayPointNum++; // waypoint number will increase with one
            }
            else
            {
                wayPointNum = 0; // waypoint number will be zero
            }
        }
    }

    private void faceToWayPoint()
    {
        // Calculate direction vector towards the waypoints
        Vector3 direction = (wayPoints[wayPointNum].transform.position - transform.position).normalized;
        // Create a target rotation based on the direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Rotate enemy towards the target rotation got before
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void EngageWithPlayer()
    {
        faceTarget(); // Fuction to Rotate the enemy towards the Player

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChasePlayerProcess(); // Process to Chase Player
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            FightWithPlayer(); // Process to Attack Player
        }
    }

    private void FightWithPlayer()
    {
        StartCoroutine(Bombing()); // Process to Attack Player
    }

    private void ChasePlayerProcess()
    {
        navMeshAgent.SetDestination(target.transform.position); // Set the NavMeshAget destination to the player
    }

    private void faceTarget()
    {
        // Calculate direction vector towards the player
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // Create a target rotation based on the direction
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        // Rotate enemy towards the target rotation got before
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chasingDistance);
        if (navMeshAgent != null)
        {
            Gizmos.DrawWireSphere(transform.position, navMeshAgent.stoppingDistance);
        }
    }

    IEnumerator Bombing()
    {
        // If canfire is true enemy can shoot
        // when enemy fire canfire will false
        if (canFire)
        {
            canFire = false;
            RayCastProcess(); 
            PlayMussleFlash();
            ProcessShootSound();
            yield return new WaitForSeconds(1);
            canFire = true;
        }

    }

    // Process to Play sound Efeect When enemy will fire
    private void ProcessShootSound()
    {
        audioSource.PlayOneShot(fireSound);
    }

    // Process to Play particle effect when enemy will fire
    private void PlayMussleFlash()
    {
        muzzleFlash.Play();
    }

    // RayCast Process for fire
    private void RayCastProcess()
    {
        RaycastHit hit;
        if (Physics.Raycast(raySphere.transform.position, raySphere.transform.forward, out hit, range))
        {
            PlayerMovement playerMovement = hit.transform.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerHealth.DecreaseHealth(amount); // To Decrease Player Health when enemy fire on player
                healthBar.TakeDamage(amount); // To Decrease Health Bar 

            }
        }
    }

    // Process for if enemy collided with any thing reset enemy's path
    void OnCollisionEnter(Collision collision)
    {
        navMeshAgent.ResetPath();
    }
}
