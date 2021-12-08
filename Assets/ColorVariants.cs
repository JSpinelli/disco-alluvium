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
    private Color nextColor;
    private bool switchColor;

    float t = 0;

    private void Awake()
    {
        myRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        SwitchToDefault();
        myCurrentIndex = myDefault;

        switchColor = false;
    }

    public void Update()
    {
        t += Time.deltaTime / 800.0f;

        if (switchColor)
        {
            if (myRenderer.color == nextColor)
            {
                switchColor = false;
                return;
            }
            myRenderer.color = Color.Lerp(myRenderer.color, nextColor, t);
        }
    }

    public void SwitchTo(int index)
    {
        myCurrentIndex = index;
        switch (index)
        {
            case 1:
            {
                nextColor = organgeShade;
                switchColor = true;
                break;
            }
            case 2:
            {
                    nextColor = purpleShade;
                    switchColor = true;
                    break;
            }
            case 3:
            {
                    nextColor = greenShade;
                    switchColor = true;
                    break;
            }
            case 4:
            {
                    nextColor = blueShade;
                    switchColor = true;
                    break;
            }
            case 5:
            {
                    nextColor = pinkShade;
                    switchColor = true;
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
                    nextColor = organgeShade;
                    switchColor = true;
                    break;
                }
            case 2:
                {
                    nextColor = purpleShade;
                    switchColor = true;
                    break;
                }
            case 3:
                {
                    nextColor = greenShade;
                    switchColor = true;
                    break;
                }
            case 4:
                {
                    nextColor = blueShade;
                    switchColor = true;
                    break;
                }
            case 5:
                {
                    nextColor = pinkShade;
                    switchColor = true;
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
