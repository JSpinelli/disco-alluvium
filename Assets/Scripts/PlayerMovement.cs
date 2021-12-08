using FMODUnity;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    
    public float sensitivity;
    public StudioEventEmitter callSound;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
        {
            
            movement.x = movement.x - sensitivity;
        }        
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = movement.x + sensitivity;
        }        
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = movement.y + sensitivity;
        }        
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = movement.y - sensitivity;
        }
        _rigidbody2D.velocity = movement;
    }
    
}
