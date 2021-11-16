using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public float movementStep = 0.000001f;
    private BezierCurve currentSection;

    private Vector3 nextPosition = new Vector3();
    public Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    private void Start()
    {
        nextPosition = path.curve[0].CalculatePosition(0.01f);
        movableThing.transform.position = path.curve[0].startPoint;
        currentSection = path.curve[0];
    }

    private void Update()
    {
        if (movableThing.transform.position != currentSection.endPoint)
        {
            if (movableThing.transform.position != nextPosition)
            {
                Vector3 objective = Vector3.MoveTowards(movableThing.transform.position, nextPosition, Time.deltaTime * speed);
                
                // Vector3 direction = objective - movableThing.transform.position;
                //Quaternion toRotation = Quaternion.FromToRotation(movableThing.transform.forward, objective);
                //movableThing.transform.rotation = Quaternion.Lerp(movableThing.transform.rotation, toRotation, speed * Time.deltaTime);
                
                //Quaternion lookOnLook = Quaternion.LookRotation(objective - movableThing.transform.position);
                //movableThing.transform.rotation =Quaternion.Slerp(movableThing.transform.rotation, lookOnLook, Time.deltaTime * speed);
                
                //movableThing.transform.LookAt(objective);
                
                movableThing.transform.position = objective;
            }
            else
            {
                Debug.Log("GIVE ME ANOTHER!");
                if (percentage > 1)
                {
                    Debug.Log("GIVE ME ANOTHER!");
                    movableThing.transform.position = currentSection.endPoint;
                    percentage = 0;
                    i++;
                    if (i == path.curve.Count)
                    {
                        Debug.Log("Reseted");
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
            Debug.Log("GIVE ME ANOTHER!");
            movableThing.transform.position = currentSection.endPoint;
            percentage = 0;
            i++;
            if (i == path.curve.Count)
            {
                Debug.Log("Reseted");
                i = 0;
            }
            currentSection = path.curve[i];
        }
    }


}

// #if UNITY_EDITOR
// [CustomEditor(typeof(BezierTraversal))]
// public class DrawBezierTraversal : Editor
// {
//     private void OnSceneViewGUI(SceneView sv)
//     {
//         BezierTraversal bt = target as BezierTraversal;
//         BezierCurve currentSection = bt.path.curve[0];
//         //bt.movableThing.transform.position = bt.path.curve[0].startPoint;
        

//     }

//     void OnEnable()
//     {
//         SceneView.duringSceneGui += OnSceneViewGUI;
//     }

//     void OnDisable()
//     {
//         SceneView.duringSceneGui -= OnSceneViewGUI;
//     }
// }
// #endif
