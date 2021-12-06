using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public GameObject player;

    public float playerAttractionRate = 1f;

    [HideInInspector] public bool attractingActive = false;
    [HideInInspector] public bool clickActive = false;

    public int amountOfNothing;
    public int amountOfAttracter;
    public int amountOfRepellers;
    public int amountOfColorChangers;
    public int amountOfOrbiters;
    
    
    public int amountOfNothingFollowing;
    public int amountOfAttracterFollowing;
    public int amountOfRepellerFollowing;
    public int amountOfColorChangersFollowing;
    public int amountOfOrbitersFollowing;

    public int amountOfBlue;
    public int amountOfOrange;
    public int amountOfPink;
    public int amountOfPurple;
    public int amountOfGreen;

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
}
