using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelationshipBehaviour : MonoBehaviour
{
    private Transform movingAwayObject;
    private bool movingAway = false;
    public float movementSpeed = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collision happening "+ other.gameObject.name);
        if (other.gameObject.CompareTag("away"))
        {
            Debug.Log("Moving Away");
            movingAway = true;
            movingAwayObject = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("away"))
        {
            movingAway = false;
            movingAwayObject = other.transform;
        }
    }

    private void Update()
    {
        if (movingAway)
        {
            Vector3 dir = (transform.position - movingAwayObject.position).normalized;
            transform.parent.position = Vector3.Lerp(transform.parent.position, transform.parent.position + (dir * movementSpeed),
                Time.deltaTime);
        }
    }
}
