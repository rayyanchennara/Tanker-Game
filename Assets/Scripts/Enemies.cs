using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach(Transform child in transform)
        {
            enemies.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
