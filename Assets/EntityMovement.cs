using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.U2D;

public class EntityMovement : MonoBehaviour
{
    public GameObject movableThing;
    public SpriteShapeController pathToMoveThrough;

    private Spline bounds;
    private Vector3[] points;
    
}
