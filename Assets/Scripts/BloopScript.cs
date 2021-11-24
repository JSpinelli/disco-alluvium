using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloopScript : MonoBehaviour
{
    private GameObject bloopGameObj;
    public GameObject bloopPrefab;
    public Vector3 scaleChange;
    private bool bloopItUp;

    // Start is called before the first frame update
    void Start()
    {
        bloopItUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BloopIt();
        }

        if (bloopItUp == true)
        {
            bloopGameObj.transform.localScale += scaleChange;
        }

        if (bloopGameObj.transform.localScale.x > 30)
        {
            bloopItUp = false;
            Destroy(bloopGameObj);
        }
    }

    public void BloopIt()
    {
        bloopGameObj = Instantiate(bloopPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        bloopItUp = true;
        GetComponent<AudioSource>().Play();
    }
}
