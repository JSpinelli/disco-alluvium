using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Camera cam;
    public AnimationCurve zoomOutCurve;
    public float zoomOutSensitivity;

    [HideInInspector]
    public GameObject player;

    public float playerAttractionRate = 1f;

    [HideInInspector] public bool attractingActive = false;
    [HideInInspector] public bool clickActive = false;

    public int amountOfNothing1;
    public int amountOfNothing2;
    public int amountOfNothing3;
    public int amountOfAttracter;
    public int amountOfRepellers;
    public int amountOfColorChangers;
    public int amountOfOrbiters;
    
    
    
    public int amountOfNothing1Following;
    public int amountOfNothing2Following;
    public int amountOfNothing3Following;
    public int amountOfAttracterFollowing;
    public int amountOfRepellerFollowing;
    public int amountOfColorChangersFollowing;
    public int amountOfOrbitersFollowing;

    public int amountOfBlue;
    public int amountOfOrange;
    public int amountOfPink;
    public int amountOfPurple;
    public int amountOfGreen;

    public int totalAmebas;
    public int amebasFollowing;
    public int amebaLimit = 50;

    public RawImage miniMap;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Should not be another class");
            Destroy(this);
        }
        
        player = GameObject.Find("Player");
        if (!player)
        {
            Debug.LogWarning("There is no GameObject named Player on this scene");
        }
    }

    private void Update()
    {
        totalAmebas = amountOfAttracter + amountOfNothing1+amountOfNothing2+amountOfNothing3 + amountOfRepellers + amountOfColorChangers;
        amebasFollowing = amountOfAttracterFollowing + amountOfNothing1Following + amountOfNothing2Following + amountOfNothing3Following+ amountOfRepellerFollowing + amountOfColorChangersFollowing;
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,3+zoomOutCurve.Evaluate(amebasFollowing)/2, Time.deltaTime * zoomOutSensitivity);
        Vector2 newSize = new Vector2(
            Mathf.Lerp(miniMap.rectTransform.rect.width, 600 - (amebasFollowing*6.5f) ,
                Time.deltaTime * zoomOutSensitivity),
            Mathf.Lerp(miniMap.rectTransform.rect.height, 600 - (amebasFollowing*6.5f) ,
                Time.deltaTime * zoomOutSensitivity)
        );
        miniMap.rectTransform.sizeDelta = newSize;
    }
}
