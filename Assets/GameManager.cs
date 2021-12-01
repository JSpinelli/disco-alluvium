using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public GameObject player;

    public float spawnTimer;

    public float playerAttractionRate = 1f;

    [HideInInspector] public bool attractingActive = false;
    
    private float timeToSpawn;

    public GameObject attracter;
    public GameObject repeller;
    public GameObject colorChanger;
    public GameObject nothing;

    public bool spawningEnabled = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Should not be another class");
            Destroy(this);
        }
        
        player = GameObject.Find("Player");
        if (!player)
        {
            Debug.LogWarning("There is no GameObject named Player on this scene");
        }
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
        Instantiate(nothing, transform.position,Quaternion.identity);
    }
}
