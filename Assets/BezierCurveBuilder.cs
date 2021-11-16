using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class BezierCurveBuilder : MonoBehaviour
{
    public List<BezierCurve> curve;

    public GameObject bezierCurve;
    public GameObject bezierCurveContinuation;
    public GameObject bezierCurveEnd;

    public void AddSection()
    {

        if (curve.Count == 0)
        {
            GameObject prefab = Instantiate(bezierCurve, transform.position, Quaternion.identity);
            prefab.transform.parent = transform;
            BezierCurve firstSection = prefab.GetComponent<BezierCurve>();
            firstSection.SetValues(transform.position);
            curve.Add(firstSection);
        }
        else
        {
            BezierCurve previous = curve[curve.Count - 1];
            GameObject prefab = Instantiate(bezierCurveContinuation, previous.endPoint, Quaternion.identity);
            prefab.transform.parent = transform;
            BezierCurveContinuation nextTrack = prefab.GetComponent<BezierCurveContinuation>();
            nextTrack.SetPrevious(previous);
            curve.Add(nextTrack);
        }
    }

    public void CloseLoop()
    {
            BezierCurve previous = curve[curve.Count - 1];
            BezierCurve first = curve[0];
            GameObject prefab = Instantiate(bezierCurveEnd, previous.endPoint, Quaternion.identity);
            prefab.transform.parent = transform;
            BezierCurveEnd end = prefab.GetComponent<BezierCurveEnd>();
            end.SetEnd(previous, first);
            curve.Add(end);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(BezierCurveBuilder))]
public class DrawBezierCurveBuilder : Editor
{
    private bool isEnabled = false;
    private bool isClosed = false;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BezierCurveBuilder curveBuilder = (BezierCurveBuilder)target;
        if(GUILayout.Button("Add Section"))
        {
            if (!isClosed){
                curveBuilder.AddSection();
            }
            
        }
        if(GUILayout.Button("Close Loop"))
        {
            isClosed = true;
            curveBuilder.CloseLoop();
        }
    }

    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurveBuilder bb = target as BezierCurveBuilder;
        if (bb.curve.Count == 0) return;
        for (int i=0; i< bb.curve.Count ; i++){
            Handles.DrawBezier(bb.curve[i].startPoint, bb.curve[i].endPoint, bb.curve[i].startTangent,bb.curve[i].endTangent, Color.blue, null, 4f);
        }
        
    }

    void OnEnable()
    {
        if (!isEnabled){
            SceneView.duringSceneGui += OnSceneViewGUI;
            isEnabled = true;
        }
        
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }

}
#endif
