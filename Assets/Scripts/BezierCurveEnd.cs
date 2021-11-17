using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BezierCurveEnd : BezierCurve
{
    public BezierCurve previous;
    public BezierCurve end;
    public float magnitudeStart = 1f;
    public float magnitudeEnd = 1f;

    public void SetEnd(BezierCurve previous, BezierCurve end)
    {
        this.previous = previous;
        this.end = end;
        this.startPoint = previous.endPoint;
        this.endPoint = end.startPoint;
        this.startTangent = this.startPoint + (Vector3.Normalize(previous.endPoint - previous.endTangent) * magnitudeStart);
        this.endTangent = this.endPoint + (Vector3.Normalize(this.endPoint - end.startTangent) * magnitudeEnd);
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(BezierCurveEnd))]
public class DrawBezierCurveEnd : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurveEnd bc = target as BezierCurveEnd;

        //bc.startTangent =bc.startPoint +  (Vector3.Normalize(bc.previous.endPoint - bc.previous.endTangent) * bc.magnitudeStart);
        bc.startPoint = bc.previous.endPoint;
        bc.endTangent = Handles.PositionHandle(bc.endTangent, Quaternion.identity);
        bc.startTangent = Handles.PositionHandle(bc.startTangent, Quaternion.identity);
        bc.endPoint =  bc.end.startPoint;
        //bc.endTangent = bc.endPoint +  (Vector3.Normalize(bc.endPoint  - bc.end.startTangent) * bc.magnitudeEnd);

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
