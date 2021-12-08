using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{

    public float spawnTimer;
    public bool spawningEnabled = false;
    public GameObject attracter;
    public GameObject repeller;
    public GameObject colorChanger;
    public GameObject[] nothing;
    private float timeToSpawn;

    private int currentSpawn;
    public Transform spawnPoint;

    private void Start()
    {
        currentSpawn = 0;
    }

    void Update()
    {
        if (spawningEnabled)
        {
            if (timeToSpawn < spawnTimer)
            {
                timeToSpawn += Time.deltaTime;
            }
            else
            {
                timeToSpawn = 0;
                Spawn();
            }   
        }
    }

    private void Spawn()
    {
        switch (currentSpawn)
        {
            case 0:
            {
                Instantiate(nothing[Random.Range(0,2)], spawnPoint.position,Quaternion.identity);
                break;
            }
            case 1:
            {
                Instantiate(attracter, spawnPoint.position,Quaternion.identity);
                break;
            }
            case 2:
            {
                Instantiate(nothing[Random.Range(0,2)], spawnPoint.position,Quaternion.identity);
                break;
            }
            case 3:
            {
                Instantiate(colorChanger, spawnPoint.position,Quaternion.identity);
                break;
            }
            case 4:
            {
                Instantiate(nothing[Random.Range(0,2)], spawnPoint.position,Quaternion.identity);
                break;
            }           
            case 5:
            {
                Instantiate(repeller, spawnPoint.position,Quaternion.identity);
                break;
            }
        }

        currentSpawn++;
        if (currentSpawn >= 6)
        {
            currentSpawn = 0;
        }
    }
}
