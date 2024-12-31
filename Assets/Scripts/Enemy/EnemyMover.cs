using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    float chasingDistance = 250f;
    float distanceToTarget = Mathf.Infinity;
    float distanceToWayPoint = Mathf.Infinity;
    float rotationSpeed = 1f;
    float range = 120f;
    int amount = 2;
    int wayPointNum = 0;

    public List<GameObject> wayPoints = new List<GameObject>();

    [SerializeField] GameObject parentOfWayPoints;
    [SerializeField] Transform target;
    [SerializeField] Transform raySphere;
    [SerializeField] ParticleSystem muzzleFlash;


    bool isProvoked;
    bool isPatrolling;
    bool canFire = true;
    NavMeshAgent navMeshAgent;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] HealthBar healthBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        foreach (Transform child in parentOfWayPoints.transform)
        {
            wayPoints.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        distanceToWayPoint = Vector3.Distance(transform.position, wayPoints[wayPointNum].transform.position);
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

        ProcessToFollowTarget();
    }

    private void ProcessToFollowTarget()
    {
        distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= chasingDistance)
        {
            isPatrolling = false;
            isProvoked = true;
        }

        else if (distanceToTarget >= chasingDistance)
        {
            isProvoked = false;
            isPatrolling = true;
        }

        if (isProvoked == true)
        {
            EngageWithPlayer();
        }

        if (isPatrolling == true)
        {
            ProcessPatrolling();
        }
    }

    private void ProcessPatrolling()
    {

        faceToWayPoint();
        navMeshAgent.SetDestination(wayPoints[wayPointNum].transform.position);
        if (distanceToWayPoint <= 80)
        {
            if (wayPointNum < wayPoints.Count - 1)
            {
                wayPointNum++;
            }
            else
            {
                wayPointNum = 0;
            }
        }
    }

    private void faceToWayPoint()
    {
        Vector3 direction = (wayPoints[wayPointNum].transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }

    private void EngageWithPlayer()
    {
        faceTarget();

        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChasePlayerProcess();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            FightWithPlayer();
        }
    }

    private void FightWithPlayer()
    {
        StartCoroutine(Bombing());
    }

    private void ChasePlayerProcess()
    {
        navMeshAgent.SetDestination(target.transform.position);
    }

    private void faceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
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
        if (canFire)
        {
            canFire = false;
            RayCastProcess();
            PlayMussleFlash();
            yield return new WaitForSeconds(1);
            canFire = true;
        }

    }

    private void PlayMussleFlash()
    {
        muzzleFlash.Play();
    }

    private void RayCastProcess()
    {
        RaycastHit hit;
        if (Physics.Raycast(raySphere.transform.position, raySphere.transform.forward, out hit, range))
        {
            PlayerMovement playerMovement = hit.transform.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                Debug.Log("Enemy Attacked You");
                playerHealth.DecreaseHealth(amount);
                healthBar.TakeDamage(amount);

            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        navMeshAgent.ResetPath();
    }
}
