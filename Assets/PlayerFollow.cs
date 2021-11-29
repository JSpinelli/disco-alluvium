using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public GameObject player;

    public float sensitivity = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 myPos = Vector2.Lerp(transform.position, player.transform.position, sensitivity * Time.deltaTime);
        Vector3 futurePos = new Vector3(myPos.x, myPos.y, transform.position.z);
        transform.position = futurePos;
    }
}
