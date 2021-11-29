using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloopAttract : MonoBehaviour
{
    public Transform player;
    private Vector3 midPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Blooper"))
        {
            Debug.Log("bloop");
            transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime);
        }
    }
}
