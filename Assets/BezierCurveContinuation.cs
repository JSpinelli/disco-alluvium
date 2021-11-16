using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BezierCurveContinuation : BezierCurve
{
    public BezierCurve previous;
    public float magnitude = 1f;

    public void SetPrevious(BezierCurve previous)
    {
        this.previous = previous; 
        this.startPoint = previous.endPoint;
        this.endPoint = previous.endPoint + new Vector3(0.0f,1f,1f);
        this.startTangent =this.startPoint +  (Vector3.Normalize(previous.endPoint - previous.endTangent) * magnitude);
        this.endTangent = previous.endPoint + new Vector3(1f,1f,1f);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(BezierCurveContinuation))]
public class DrawBezierCurveContinuation : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurveContinuation bc = target as BezierCurveContinuation;

        bc.endPoint = Handles.PositionHandle(bc.endPoint, Quaternion.identity);
        bc.endTangent = Handles.PositionHandle(bc.endTangent, Quaternion.identity);

        bc.startTangent =bc.startPoint +  (Vector3.Normalize(bc.previous.endPoint - bc.previous.endTangent) * bc.magnitude);
        bc.startPoint = bc.previous.endPoint;

        Handles.DrawBezier(bc.startPoint, bc.endPoint, bc.startTangent, bc.endTangent, Color.red, null, 2f);
    }

    void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneViewGUI;
    }

    void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneViewGUI;
    }
}
#endif
