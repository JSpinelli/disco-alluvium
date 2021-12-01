using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerMovement : MonoBehaviour
{
    public float sensitivity;
    private SpriteShapeRenderer asd;
    public CircleCollider2D collider;
    public float attractingCooldown;
    private float attractingCooldownTimer;
    private float attractingTimer;
    public float attractingDuration;
    
    private GameObject bloopGameObj;
    public GameObject bloopPrefab;
    public Vector3 scaleChange;

    private bool bloopItUp = false;

    private Rigidbody2D _rigidbody2D;

    private Vector3 targetPosition;

    public StudioEventEmitter callSound;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        attractingCooldownTimer = attractingCooldown;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Timers();
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

        

        if (Input.GetKeyDown(KeyCode.Space) && attractingCooldownTimer >= attractingCooldown)
        {
            GameManager.instance.attractingActive = true;
            callSound.Play();
            BloopIt();
            collider.enabled = true;
            attractingCooldownTimer = 0;
            attractingTimer = 0;
        }
        
        if (bloopItUp)
        {
            bloopGameObj.transform.localScale += scaleChange;

            if (bloopGameObj.transform.localScale.x > 30)
            {
                bloopItUp = false;
                Destroy(bloopGameObj);
            }
        }

        _rigidbody2D.velocity = movement;
    }

    private void Timers()
    {
        if (attractingCooldownTimer < attractingCooldown)
        {
            attractingCooldownTimer += Time.deltaTime;
        }

        if (attractingTimer < attractingDuration)
        {
            attractingTimer += Time.deltaTime;
        }
        else
        {
            collider.enabled = false;
            GameManager.instance.attractingActive = false;
        }
    }
    
    public void BloopIt()
    {
        bloopGameObj = Instantiate(bloopPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bloopItUp = true;
    }
}
