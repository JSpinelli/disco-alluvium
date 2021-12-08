using UnityEngine;

public class MouseTracker : MonoBehaviour
{
    public Camera cam;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        Vector3 newPos = new Vector3(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y, transform.position.z);
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<MouseTrackerHit>().myBehaviour.InMouseArea();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        other.GetComponent<MouseTrackerHit>().myBehaviour.OutOfMouseArea();
    }
}
