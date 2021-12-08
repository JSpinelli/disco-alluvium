using UnityEngine;

public class PlayerFollow : MonoBehaviour
{

    public GameObject player;

    public float sensitivity = 2;
    void Update()
    {
        Vector2 myPos = Vector2.Lerp(transform.position, player.transform.position, sensitivity * Time.deltaTime);
        Vector3 futurePos = new Vector3(myPos.x, myPos.y, transform.position.z);
        transform.position = futurePos;
    }
}
