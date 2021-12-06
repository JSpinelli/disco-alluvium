using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float spawnTimer;
    public bool spawningEnabled = false;
    public GameObject attracter;
    public GameObject repeller;
    public GameObject colorChanger;
    public GameObject nothing;
    private float timeToSpawn;

    public Transform spawnPoint;
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
        Instantiate(nothing, spawnPoint.position,Quaternion.identity);
    }
}
