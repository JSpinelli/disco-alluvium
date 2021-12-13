using System;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class CollisionDetection : MonoBehaviour
{
    public float timeToSpawnNext;
    private bool _readyToSpawn = false;
    private float _timer;

    public RelationshipBehaviour myBehaviour;
    public StudioEventEmitter collisionSound;

    private Rigidbody2D _myRigidbody2D;

    private void Start()
    {
        timeToSpawnNext = Spawner.Instance.spawnTimer;
        _myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player")) return;
        if (other.gameObject.CompareTag("Wall") && !myBehaviour.followingPlayer)
        {
            Vector3 dir = transform.position - other.gameObject.transform.position;
            _myRigidbody2D.AddForce(dir.normalized * 10, ForceMode2D.Impulse);
            return;
        }
        if (!myBehaviour.followingPlayer)
            collisionSound.Play();
        if (!other.gameObject.CompareTag(gameObject.tag) && _readyToSpawn)
        {
            if (Random.Range(0f, 1f) < Spawner.Instance.spawnChance)
            {
                Spawner.Instance.Spawn(other.GetContact(0).point);
                _readyToSpawn = false;
                timeToSpawnNext = Spawner.Instance.spawnTimer;   
            }
        }
    }

    private void Update()
    {
        if (!_readyToSpawn)
        {
            if (timeToSpawnNext > _timer)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _readyToSpawn = true;
                _timer = 0;
            }   
        }
    }
}
