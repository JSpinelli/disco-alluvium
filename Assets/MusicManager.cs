using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private StudioEventEmitter musicPlayer;

    private ParamRef amountOfNothing;

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

        GameObject player = GameObject.Find("Player");
        if (!player)
        {
            Debug.LogWarning("There is no GameObject named Player on this scene");
        }
        else
        {
            musicPlayer = player.GetComponents<StudioEventEmitter>()[1];
        }
    }
    void Update()
    {
        if (GameManager.instance.amountOfNothingFollowing > 0)
            musicPlayer.EventInstance.setParameterByName("BPM",
                GameManager.instance.amountOfNothing / GameManager.instance.amountOfNothingFollowing);
        else
            musicPlayer.EventInstance.setParameterByName("BPM", 0);

        if (GameManager.instance.amountOfAttracterFollowing > 0)
            musicPlayer.EventInstance.setParameterByName("Melody Instrument",
                GameManager.instance.amountOfAttracter / GameManager.instance.amountOfAttracterFollowing);
        else
            musicPlayer.EventInstance.setParameterByName("Melody Instrument", 0);

        if (GameManager.instance.amountOfRepellerFollowing > 0)
            musicPlayer.EventInstance.setParameterByName("Chord Progression Instrument",
                GameManager.instance.amountOfRepellers / GameManager.instance.amountOfRepellerFollowing);
        else
            musicPlayer.EventInstance.setParameterByName("Chord Progression Instrument",0);
    }
}