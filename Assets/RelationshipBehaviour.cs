using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipBehaviour : MonoBehaviour
{
    public enum Types{
        Repel,
        Attract,
        ColorChange,
        Orbit,
        Nothing
    }
    private Dictionary<int, Transform> attractingObjects;
    private Dictionary<int, Transform> repellingObjects;
    
    public float movementSpeed = 2f;
    public Types myTag;

    public EntityBehaviour myBehaviour;
    public SpriteRenderer myRenderer;

    [HideInInspector]
    public bool swapped = false;

    private Rigidbody2D entityRigidbody2D;

    private void Start()
    {
        attractingObjects = new Dictionary<int, Transform>();
        repellingObjects = new Dictionary<int, Transform>();
        entityRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision happening "+ other.gameObject.name);
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel") && myTag != Types.Repel)
        {
            Debug.Log(transform.name+ " repelled by "+thing.name);
            repellingObjects.Add(other.gameObject.GetInstanceID(),thing.transform);
        }

        if (thing.CompareTag("Attract") && myTag != Types.Attract)
        {
            Debug.Log(transform.name+ " attracted by "+thing.name);
            attractingObjects.Add(thing.GetInstanceID(),thing.transform);
        }

        if (thing.CompareTag("Orbit") && myTag != Types.Orbit && !swapped)
        {
            swapped = true;
            if (myBehaviour)
            {
                myBehaviour.player = thing;
                myBehaviour.Switch();   
            }
        }

        if (thing.CompareTag("ColorChange") && myTag != Types.ColorChange)
        {
            // Should Swap Sprites
            //myRenderer.sprite
        }

        if (thing.CompareTag("Player"))
        {
            attractingObjects.Add(thing.GetInstanceID(),thing.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel"))
        {
            repellingObjects.Remove(thing.GetInstanceID());
        }
        
        if (thing.CompareTag("Attract"))
        {
            attractingObjects.Remove(thing.GetInstanceID());
        }

        if (thing.CompareTag("Player"))
        {
            attractingObjects.Remove(thing.GetInstanceID());
        }

    }

    private void Update()
    {
        Repel();
        Attract();
    }

    private void Repel()
    {
        if (repellingObjects.Count == 0) return; 
        Vector2 mainDir = Vector2.zero;
        foreach (var obj in repellingObjects)
        {
            Vector2 dir = (transform.position - obj.Value.position).normalized;
            mainDir = mainDir + dir;
        }
        mainDir = mainDir / repellingObjects.Count;
        entityRigidbody2D.velocity = entityRigidbody2D.velocity + (mainDir * movementSpeed);    
        // transform.parent.position = Vector3.Lerp(transform.parent.position, transform.parent.position + (mainDir * movementSpeed),
        //     Time.deltaTime);
    }

    private void Attract()
    {
        if (attractingObjects.Count == 0) return; 
        Vector2 mainDir = Vector2.zero;
        foreach (var obj in attractingObjects)
        {
            Vector2 dir = (obj.Value.position - transform.position).normalized;
            mainDir = mainDir + dir;
        }
        mainDir = mainDir / attractingObjects.Count;
        entityRigidbody2D.velocity = entityRigidbody2D.velocity + (mainDir * movementSpeed);
        // transform.parent.position = Vector3.Lerp(transform.parent.position, transform.parent.position + (mainDir * movementSpeed),
        //     Time.deltaTime);
    }
}
