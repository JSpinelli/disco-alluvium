using FMODUnity;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [ParamRef] public string percussion1;
    [ParamRef] public string percussion2;
    [ParamRef] public string percussion3;
    [ParamRef] public string attracterInstrument;
    [ParamRef] public string repellerInstrument;
    [ParamRef] public string colorChangerInstrument;
    [ParamRef] public string dominantChord;
    [ParamRef] public string melodyVolume;
    [ParamRef] public string percussionVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.Log("Should not be another class");
            Destroy(this);
        }
    }

    void Update()
    {
        int percussionFollowing = GameManager.instance.amountOfNothing1Following +
                                  GameManager.instance.amountOfNothing2Following +
                                  GameManager.instance.amountOfNothing3Following;
        int melodyFollowing = GameManager.instance.amountOfAttracterFollowing +
                             GameManager.instance.amountOfRepellerFollowing +
                             GameManager.instance.amountOfColorChangersFollowing;
        
        RuntimeManager.StudioSystem.setParameterByName(percussionVolume,percussionFollowing);
        RuntimeManager.StudioSystem.setParameterByName(melodyVolume,melodyFollowing);

        if (melodyFollowing == 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(attracterInstrument,0);


            RuntimeManager.StudioSystem.setParameterByName(repellerInstrument,0);


            RuntimeManager.StudioSystem.setParameterByName(colorChangerInstrument,0);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(attracterInstrument,
                (float) GameManager.instance.amountOfAttracterFollowing / melodyFollowing);


            RuntimeManager.StudioSystem.setParameterByName(repellerInstrument,
                (float) GameManager.instance.amountOfRepellerFollowing / melodyFollowing);


            RuntimeManager.StudioSystem.setParameterByName(colorChangerInstrument,
                (float) GameManager.instance.amountOfColorChangersFollowing / melodyFollowing);
        }

        if (percussionFollowing == 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion1,0);


            RuntimeManager.StudioSystem.setParameterByName(percussion2,0);

            RuntimeManager.StudioSystem.setParameterByName(percussion3,0);
            
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(percussion1,
                (float) GameManager.instance.amountOfNothing1Following / percussionFollowing);


            RuntimeManager.StudioSystem.setParameterByName(percussion2,
                (float) GameManager.instance.amountOfNothing2Following / percussionFollowing);

            RuntimeManager.StudioSystem.setParameterByName(percussion3,
                (float) GameManager.instance.amountOfNothing3Following / percussionFollowing);   
        }


        if (GameManager.instance.totalAmebas > 0)
        {
            RuntimeManager.StudioSystem.setParameterByName(dominantChord,
                (float) GameManager.instance.amebasFollowing / GameManager.instance.totalAmebas);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName(dominantChord, 0);
        }
    }
}