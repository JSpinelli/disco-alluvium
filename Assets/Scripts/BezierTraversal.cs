using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BezierTraversal : MonoBehaviour
{
    public BezierCurveBuilder path;

    public GameObject movableThing;
    public float speed = 10f;
    int i = 0;
    float percentage = 0;
    public float movementStep = 0.1f;
    private BezierCurve currentSection;

    private bool moving = false;

    private Vector3 nextPosition = new Vector3();

    private Vector3 prevPos;

    private void Start()
    {
        nextPosition = path.curve[0].CalculatePosition(0.01f);
        movableThing.transform.position = path.curve[0].startPoint;
        currentSection = path.curve[0];
        prevPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            if (prevPos != transform.position)
            {
                movableThing.transform.position = movableThing.transform.position + (transform.position - prevPos);
                prevPos = transform.position;
                nextPosition = currentSection.CalculatePosition(percentage);
            }
            if (movableThing.transform.position != currentSection.endPoint)
        {
            if (movableThing.transform.position != nextPosition)
            {
                Vector3 objective = Vector3.MoveTowards(movableThing.transform.position, nextPosition, Time.deltaTime * currentSection.spreadThroughSection.Evaluate(percentage) *currentSection.speedThroughSection);
                movableThing.transform.position = objective;
            }
            else
            {
                if (percentage > 1)
                {
                    movableThing.transform.position = currentSection.endPoint;
                    percentage = 0;
                    i++;
                    if (i == path.curve.Count)
                    {
                        i = 0;
                    }
                    currentSection = path.curve[i];
                }
                percentage += movementStep;
                nextPosition = currentSection.CalculatePosition(percentage);
            }
        }
        else
        {
            movableThing.transform.position = currentSection.endPoint;
            percentage = 0;
            i++;
            if (i == path.curve.Count)
            {
                i = 0;
            }
            currentSection = path.curve[i];
        } 
        }
        
    }

    public void StopResume()
    {
        moving = !moving;
        Debug.Log(moving? gameObject.name+" Enabled": gameObject.name+ " Disabled");
    }


}
