using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float _timeToSpawn;
    private int _currentSpawn;
    
    public float spawnTimer;
    public bool spawningEnabled = false;
    public GameObject attracter;
    public GameObject repeller;
    public GameObject colorChanger;
    public GameObject[] nothing;
    public Transform spawnPoint;


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

    private void Spawn()
    {
        switch (_currentSpawn)
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

        _currentSpawn++;
        if (_currentSpawn >= 6)
        {
            _currentSpawn = 0;
        }
    }
}
