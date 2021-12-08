using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    private StudioEventEmitter musicPlayer;

    [FMODUnity.ParamRef] public string percussion1;
    [FMODUnity.ParamRef] public string percussion2;
    [FMODUnity.ParamRef] public string percussion3;
    [FMODUnity.ParamRef] public string melodyInstrument;
    [FMODUnity.ParamRef] public string minorChordInstrument;
    [FMODUnity.ParamRef] public string majorChordInstrument;
    [FMODUnity.ParamRef] public string dominantChord;

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
        if (GameManager.instance.amountOfNothing1 > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion1,
                (float)GameManager.instance.amountOfNothing1Following /  GameManager.instance.amountOfNothing1);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion1,0);
        }
        if (GameManager.instance.amountOfNothing2 > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion2,
                (float) GameManager.instance.amountOfNothing2Following / GameManager.instance.amountOfNothing2);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion2,0);
        }        
        if (GameManager.instance.amountOfNothing3 > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion3,
                (float) GameManager.instance.amountOfNothing3Following / GameManager.instance.amountOfNothing3);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion3,0);
        }

        if (GameManager.instance.amountOfAttracter > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(melodyInstrument,
                (float) GameManager.instance.amountOfAttracterFollowing / GameManager.instance.amountOfAttracter); 
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(melodyInstrument,0); 
        }

        if (GameManager.instance.amountOfRepellers > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(minorChordInstrument,(float) GameManager.instance.amountOfRepellerFollowing / GameManager.instance.amountOfRepellers); 
            RuntimeManager.StudioSystem.setParameterByName(majorChordInstrument,(float) GameManager.instance.amountOfRepellerFollowing / GameManager.instance.amountOfRepellers); 
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(minorChordInstrument,0); 
            RuntimeManager.StudioSystem.setParameterByName(majorChordInstrument,0); 
        }
        
        if (GameManager.instance.totalAmebas > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(dominantChord,(float)GameManager.instance.amebasFollowing / GameManager.instance.totalAmebas);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(dominantChord,0);
        }
        
        
    }
}