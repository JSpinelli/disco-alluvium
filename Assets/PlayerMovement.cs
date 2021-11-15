using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float sensitivity;

    private Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.A))
        {
            movement.x = movement.x - sensitivity;
        }        
        if (Input.GetKeyDown(KeyCode.D))
        {
            movement.x = movement.x + sensitivity;
        }        
        if (Input.GetKeyDown(KeyCode.W))
        {
            movement.y = movement.y + sensitivity;
        }        
        if (Input.GetKeyDown(KeyCode.S))
        {
            movement.y = movement.y - sensitivity;
        }

        targetPosition = targetPosition + movement;
        transform.position = Vector3.Lerp(transform.position, targetPosition,Time.deltaTime);
    }
}
