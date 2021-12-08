using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrackerHit : MonoBehaviour
{
    public RelationshipBehaviour myBehaviour;
    // Start is called before the first frame update
    void Start()
    {
        myBehaviour = gameObject.GetComponentInChildren<RelationshipBehaviour>();
    }
}
