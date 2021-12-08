using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoZoom : MonoBehaviour
{
    int numFollowers;
    GameManager gm;
    public float divisor;
    public float startSize;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {        
        numFollowers = gm.amountOfAttracterFollowing + gm.amountOfColorChangersFollowing + gm.amountOfNothing1Following + gm.amountOfOrbitersFollowing;

        if(numFollowers <= 30)
        {
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, (numFollowers / divisor) + startSize, Time.deltaTime);
        }
    }
}
