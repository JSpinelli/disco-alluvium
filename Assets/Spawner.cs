using UnityEngine;

public class Spawner : MonoBehaviour
{

    public static Spawner Instance;
    
    private float _timeToSpawn;
    private int _currentSpawn;
    
    public float spawnTimer;
    public bool spawningEnabled = false;
    public GameObject attracter;
    public GameObject repeller;
    public GameObject colorChanger;
    public GameObject[] nothing;
    public Transform spawnPoint;

    public float globalSpawnTime = 5;
    public float spawnChance = 0.5f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Should not be another class");
            Destroy(this);
        }
    }

    private void Start()
    {
        _currentSpawn = 0;
        _timeToSpawn = 0;
    }

    void Update()
    {
        if (spawningEnabled)
        {
            if (_timeToSpawn < spawnTimer)
            {
                _timeToSpawn += Time.deltaTime;
            }
            else
            {
                _timeToSpawn = 0;
                Spawn();
            }   
        }
    }

    public void Spawn(Vector3 spawnPos)
    {
        if (GameManager.instance.totalAmebas > GameManager.instance.amebaLimit) return;
        switch (_currentSpawn)
        {
            case 0:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPos,Quaternion.identity);
                break;
            }
            case 1:
            {
                Instantiate(attracter, spawnPos,Quaternion.identity);
                break;
            }
            case 2:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPos,Quaternion.identity);
                break;
            }
            case 3:
            {
                Instantiate(colorChanger, spawnPos,Quaternion.identity);
                break;
            }
            case 4:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPos,Quaternion.identity);
                break;
            }           
            case 5:
            {
                Instantiate(repeller, spawnPos,Quaternion.identity);
                break;
            }
        }

        globalSpawnTime = globalSpawnTime + 10;
        _currentSpawn++;
        if (_currentSpawn >= 6)
        {
            _currentSpawn = 0;
        }
    }
    private void Spawn()
    {
        switch (_currentSpawn)
        {
            case 0:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPoint.position,Quaternion.identity);
                break;
            }
            case 1:
            {
                Instantiate(attracter, spawnPoint.position,Quaternion.identity);
                break;
            }
            case 2:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPoint.position,Quaternion.identity);
                break;
            }
            case 3:
            {
                Instantiate(colorChanger, spawnPoint.position,Quaternion.identity);
                break;
            }
            case 4:
            {
                Instantiate(nothing[Random.Range(0,3)], spawnPoint.position,Quaternion.identity);
                break;
            }           
            case 5:
            {
                Instantiate(repeller, spawnPoint.position,Quaternion.identity);
                break;
            }
        }

        _currentSpawn++;
        if (_currentSpawn >= 6)
        {
            _currentSpawn = 0;
        }
    }
}
