using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif
public class BezierTraversal : MonoBehaviour
{
    public BezierCurveBuilder path;

    public GameObject movableThing;
    public float speed = 1f;
    public float offset = 0f;
    int i = 0;
    float percentage = 0;
    public float movementStep = 0.1f;
    private BezierCurve currentSection;

    public bool moving = false;

    private Vector3 nextPosition = new Vector3();

    private Vector3 prevPos;

    private void Start()
    {
        if (path.curve.Count != 0)
        {
            nextPosition = path.curve[0].CalculatePosition(0.01f);
            movableThing.transform.position = path.curve[0].startPoint;
            currentSection = path.curve[0];
        }

        prevPos = transform.position;
        percentage = offset;
    }

    private void FixedUpdate()
    {
        if (path.curve.Count == 0) return;
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
                if (Vector3.Distance(movableThing.transform.position,nextPosition) > 0.01f)
                {
                    Vector3 objective = Vector3.MoveTowards(movableThing.transform.position, nextPosition,
                        Time.deltaTime * currentSection.spreadThroughSection.Evaluate(percentage) *
                        currentSection.speedThroughSection * speed);
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
    }
}