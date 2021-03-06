using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour
{
    public Vector3 startPoint = new Vector3(-0.0f, 0.0f, 0.0f);
    public Vector3 endPoint = new Vector3(-2.0f, 2.0f, 0.0f);
    public Vector3 startTangent = Vector3.zero;
    public Vector3 endTangent = Vector3.zero;

    private Vector3 startPointOG;
    private Vector3 endPointOG;
    private Vector3 startTangentOG;
    private Vector3 endTangentOG;
    
    public float speedThroughSection = 10;
    public AnimationCurve spreadThroughSection = AnimationCurve.Constant(0,1,1);

    private Vector3 previousParentPos = Vector3.zero;

    public void SetValues(Vector3 position)
    {
        Vector3 papa = Vector3.zero;// transform.parent.position;
        startPoint = papa + position;
        endPoint = papa+ position + new Vector3(0.0f,1f,0f);
        endTangent = papa+ position + new Vector3(1f,1f,1f);
    }

    public Vector3 CalculatePosition(float t){
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;
        
        Vector3 p = uuu * startPoint; 
        p += 3 * uu * t * startTangent; 
        p += 3 * u * tt * endTangent; 
        p += ttt * endPoint; 
        
        return p;
    }
    
    

    public void UpdatePos()
    {
        if (previousParentPos == transform.parent.position)
            return;

        Vector3 diff = transform.parent.position - previousParentPos;
        startPoint = diff + startPoint;
        endPoint = diff + endPoint;
        startTangent = diff + startTangent;
        endTangent = diff + endTangent;
        previousParentPos = transform.parent.position;
    }

    public void ResetPos()
    {
        Vector3 diff = previousParentPos;
        startPoint = diff + startPoint;
        endPoint = diff + endPoint;
        startTangent = diff + startTangent;
        endTangent = diff + endTangent;
        previousParentPos = transform.parent.position;
    }
}


#if UNITY_EDITOR
[CustomEditor(typeof(BezierCurve))]
public class DrawBezierCurve : Editor
{
    private void OnSceneViewGUI(SceneView sv)
    {
        BezierCurve be = target as BezierCurve;
        be.startPoint = Handles.PositionHandle(be.startPoint, Quaternion.identity);
        be.endPoint = Handles.PositionHandle(be.endPoint, Quaternion.identity);
        be.startTangent = Handles.PositionHandle(be.startTangent, Quaternion.identity);
        be.endTangent = Handles.PositionHandle(be.endTangent, Quaternion.identity);

        Handles.DrawBezier(be.startPoint, be.endPoint, be.startTangent, be.endTangent, Color.red, null, 2f);
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
