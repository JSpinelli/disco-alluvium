using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Random = UnityEngine.Random;

public class RelationshipBehaviour : MonoBehaviour
{
    public enum Types{
        Repel,
        Attract,
        ColorChange,
        Orbit,
        Nothing
    }
    [HideInInspector]
    public int myColorIndex;

    public SpriteRenderer myRenderer;
    public Color notFollowingColorOutline = Color.white;
    public Color followingColorOutline = Color.blue;
    public ColorVariants myColor;
    public float movementSpeed = 0.01f;
    public Types myTag;
    public StudioEventEmitter clickAdd;
    public StudioEventEmitter clickRemove;

    private float clickTimer = 0.2f;
    private float _timeToClick;
    private TrailRenderer _myTrailRenderer;
    private Material _myMat;
    private bool _inMouseArea;
    private Rigidbody2D _entityRigidbody2D;
    private bool _followingPlayer;
    private Dictionary<int, Transform> _attractingObjects;
    private Dictionary<int, Transform> _repellingObjects;

    private void Start()
    {
        _timeToClick = 0f;
        _attractingObjects = new Dictionary<int, Transform>();
        _repellingObjects = new Dictionary<int, Transform>();
        _entityRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        myColorIndex = Random.Range(1, 5);
        _myTrailRenderer = GetComponent<TrailRenderer>();
        switch (myTag)
        {
            case Types.Attract:
            {
                GameManager.instance.amountOfAttracter++;
                break;
            }
            case Types.Nothing:
            {
                switch (gameObject.name)
                {
                    case "Simple1":
                    {
                        GameManager.instance.amountOfNothing1++;
                        break;
                    }
                    case "Simple2":
                    {
                        GameManager.instance.amountOfNothing2++;
                        break;
                    }
                    case "Simple3":
                    {
                        GameManager.instance.amountOfNothing3++;
                        break;
                    }
                }
                break;
            }
            case Types.ColorChange:
            {
                GameManager.instance.amountOfColorChangers++;
                break;
            }
            case Types.Repel:
            {
                GameManager.instance.amountOfRepellers++;
                break;
            }
            case Types.Orbit:
            {
                GameManager.instance.amountOfOrbiters++;
                break;
            }
        }

        _myMat = myRenderer.material;
        _myTrailRenderer.enabled = false;
    }
    
        
    private void Update()
    {
        Repel();
        Attract();
        if (_inMouseArea)
        {
            if (Input.GetMouseButton(0) && _timeToClick > clickTimer)
            {
                _timeToClick = 0;
                _followingPlayer = !_followingPlayer;
                if (_followingPlayer)
                {
                    clickAdd.Play();
                    _myMat.SetColor("_SolidOutline",followingColorOutline);
                    _myTrailRenderer.enabled = true;
                    transform.parent.gameObject.layer = 7;
                    AddMeToTypeCount();
                }
                else
                {
                    clickRemove.Play();
                    _myMat.SetColor("_SolidOutline",notFollowingColorOutline);
                    _myTrailRenderer.enabled = false;
                    RemoveMeFromTypeCount();
                    transform.parent.gameObject.layer = 8; 
                }
            }
        }
        if (_timeToClick < clickTimer)
        {
            _timeToClick += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel") && myTag != Types.Repel)
        {
            _repellingObjects.Add(other.gameObject.GetInstanceID(),thing.transform);
        }

        if (thing.CompareTag("Attract") && myTag != Types.Attract)
        {
            _attractingObjects.Add(thing.GetInstanceID(),thing.transform);
        }
        
        if (thing.CompareTag("ColorChange")&& (myTag != Types.ColorChange))
        {
            if (myColor != null)
            {
                RelationshipBehaviour otherBehaviour = thing.GetComponent<RelationshipBehaviour>();
                myColor.SwitchTo(otherBehaviour.myColorIndex);
                ChangeColorAmount();
            }
        }
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel"))
        {
            _repellingObjects.Remove(thing.GetInstanceID());
        }
        
        if (thing.CompareTag("Attract"))
        {
            _attractingObjects.Remove(thing.GetInstanceID());
        }
    }

    public void InMouseArea()
    {
        _inMouseArea = true;
        _myMat.SetFloat("_OutlineEnabled",1);
    }

    public void OutOfMouseArea()
    {
        _inMouseArea = false;
        _myMat.SetFloat("_OutlineEnabled",0);
    }

    private void AddMeToTypeCount()
    {
        switch (myTag)
        {
            case Types.Attract:
            {
                GameManager.instance.amountOfAttracterFollowing++;
                break;
            }
            case Types.Nothing:
            {
                switch (gameObject.name)
                {
                    case "Simple1":
                    {
                        GameManager.instance.amountOfNothing1Following++;
                        break;
                    }
                    case "Simple2":
                    {
                        GameManager.instance.amountOfNothing2Following++;
                        break;
                    }
                    case "Simple3":
                    {
                        GameManager.instance.amountOfNothing3Following++;
                        break;
                    }
                }
                break;
            }
            case Types.ColorChange:
            {
                GameManager.instance.amountOfColorChangersFollowing++;
                break;
            }
            case Types.Repel:
            {
                GameManager.instance.amountOfRepellerFollowing++;
                break;
            }
            case Types.Orbit:
            {
                GameManager.instance.amountOfOrbitersFollowing++;
                break;
            }
        }
    }

    private void ChangeColorAmount()
    {
        if (myColor == null) return;
        switch (myColor.myCurrentIndex)
        {
            case 1:
            {
                GameManager.instance.amountOfOrange++;
                break;
            }
            case 2:
            {
                GameManager.instance.amountOfPurple++;
                break;
            }
            case 3:
            {
                GameManager.instance.amountOfGreen++;
                break;
            }
            case 4:
            {
                GameManager.instance.amountOfBlue++;
                break;
            }
            case 5:
            {
                GameManager.instance.amountOfPink++;
                break;
            }
        }
    }

    private void RemoveMeFromTypeCount()
    {
        switch (myTag)
        {
            case Types.Attract:
            {
                GameManager.instance.amountOfAttracterFollowing--;
                break;
            }
            case Types.Nothing:
            {
                switch (gameObject.name)
                {
                    case "Simple1":
                    {
                        GameManager.instance.amountOfNothing1Following--;
                        break;
                    }
                    case "Simple2":
                    {
                        GameManager.instance.amountOfNothing2Following--;
                        break;
                    }
                    case "Simple3":
                    {
                        GameManager.instance.amountOfNothing3Following--;
                        break;
                    }
                }
                break;
            }
            case Types.ColorChange:
            {
                GameManager.instance.amountOfColorChangersFollowing--;
                break;
            }
            case Types.Repel:
            {
                GameManager.instance.amountOfRepellerFollowing--;
                break;
            }
            case Types.Orbit:
            {
                GameManager.instance.amountOfOrbitersFollowing--;
                break;
            }
        }
    }

    private void Repel()
    {
        if (_repellingObjects.Count == 0) return; 
        Vector2 mainDir = Vector2.zero;
        foreach (var obj in _repellingObjects)
        {
            Vector2 dir = transform.position - obj.Value.position;
            float dist = Vector2.Distance(obj.Value.position, transform.position);
            mainDir += dir.normalized * (10 * (1/dist));
        }
        mainDir = mainDir / _repellingObjects.Count;
        
        _entityRigidbody2D.velocity += (mainDir * movementSpeed);
    }

    private void Attract()
    {
        Vector2 mainDir = Vector2.zero;
        if (_attractingObjects.Count != 0)
        {
            
            foreach (var obj in _attractingObjects)
            {
                Vector2 dir = (obj.Value.position - transform.position);
                float dist = Vector2.Distance(obj.Value.position, transform.position);
                mainDir += (dir.normalized*dist);
            }

            mainDir = mainDir / _attractingObjects.Count;
        }
        if (_followingPlayer)
        {
            Vector2 dir = (GameManager.instance.player.transform.position - transform.position);
            float dist = Vector2.Distance(GameManager.instance.player.transform.position, transform.position);
            mainDir = mainDir + (dir.normalized * (GameManager.instance.playerAttractionRate * dist));
        }
        _entityRigidbody2D.velocity += (mainDir * movementSpeed);
    }
}
