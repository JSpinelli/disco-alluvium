using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterExplosioner : MonoBehaviour
{
    public GameObject centerBug;
    public Transform parent;
    public int explosionStrength;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "HornCollider")
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }

            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).GetComponent<Rigidbody2D>().AddForce(new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode2D.Impulse);
                transform.GetChild(i).SetParent(parent);
            }
        }
    }
}