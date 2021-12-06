using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColorVariants : MonoBehaviour
{

    public Color organgeShade;
    public Color purpleShade;
    public Color greenShade;
    public Color blueShade;
    public Color pinkShade;

    public int myDefault;

    [HideInInspector]
    public int myCurrentIndex;

    public SpriteRenderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SwitchToDefault();
        myCurrentIndex = myDefault;
    }

    public void SwitchTo(int index)
    {
        myCurrentIndex = index;
        switch (index)
        {
            case 1:
            {
                myRenderer.color = organgeShade;
                break;
            }
            case 2:
            {
                myRenderer.color = purpleShade;
                break;
            }
            case 3:
            {
                myRenderer.color = greenShade;
                break;
            }
            case 4:
            {
                myRenderer.color = blueShade;
                break;
            }
            case 5:
            {
                myRenderer.color = pinkShade;
                break;
            }
            default:
            {
                myRenderer.color = organgeShade;
                break;
            }
        }
    }

    public void SwitchToDefault()
    {
        switch (myDefault)
        {
            case 1:
            {
                myRenderer.color = organgeShade;
                break;
            }
            case 2:
            {
                myRenderer.color = purpleShade;
                break;
            }
            case 3:
            {
                myRenderer.color = greenShade;
                break;
            }
            case 4:
            {
                myRenderer.color = blueShade;
                break;
            }
            case 5:
            {
                myRenderer.color = pinkShade;
                break;
            }
            default:
            {
                myRenderer.color = organgeShade;
                break;
            }
        }
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorVariants))]
public class DrawColorVariant : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ColorVariants colorVariants = (ColorVariants)target;
        if(GUILayout.Button("Check Default"))
        {
            colorVariants.SwitchToDefault();
        }
    }
}
#endif
