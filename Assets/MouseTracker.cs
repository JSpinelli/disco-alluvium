using System;
using UnityEditor.AssetImporters;
using UnityEngine;
using UnityEngine.Animations;

public class MouseTracker : MonoBehaviour
{
    public Camera cam;
    public CircleCollider2D collider;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        collider.enabled = true;
    }

    private void Update()
    {
        Vector3 newPos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        transform.position = newPos;

        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.clickActive = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.instance.clickActive = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<MouseTrackerHit>().myBehaviour.StartLerp();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<MouseTrackerHit>().myBehaviour.StopLerp();
    }
}
