using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class IndicatorScript : MonoBehaviour
{
    [SerializeField] GameObject enemyParentObject;
    [SerializeField] GameObject lastArrow;
    [SerializeField] Transform target;
    [SerializeField] Transform player;
    [SerializeField] TextMeshProUGUI hintText;
    GameObject nearestEnemy;
    float lowDistance;
    float rotationSpeed = 2f;
    public List<GameObject> enemies = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hintText.enabled = false;
        foreach (Transform child in enemyParentObject.transform)
        {
            enemies.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyDestroyCheck();
    }

    private void ProcessDirection()
    {
        GetNearestEnemy();
    }

    private void GetNearestEnemy()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i].gameObject != null && player != null)
            {
                float distance = Vector3.Distance(player.transform.position, enemies[i].transform.position);
                if (nearestEnemy == null)
                {
                    nearestEnemy = enemies[i].gameObject;
                    lowDistance = distance;
                }
                else
                {
                    if (lowDistance > distance)
                    {
                        lowDistance = distance;
                        nearestEnemy = enemies[i].gameObject;
                    }
                }
            }
        }
        if (nearestEnemy != null)
        {
            faceToWayPoint(nearestEnemy);
        }
        else
        {
            hintText.enabled = true;
            faceToWayPoint(lastArrow);
        }
    }

    private void EnemyDestroyCheck()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[0].gameObject != null)
            {

            }
            else
            {
                enemies.Clear();
                foreach (Transform child in enemyParentObject.transform)
                {
                    enemies.Add(child.gameObject);
                }
            }
        }
        ProcessDirection();
    }

    private void faceToWayPoint(GameObject enemy)
    {
        Vector3 direction = (enemy.transform.position - target.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
    }
}
