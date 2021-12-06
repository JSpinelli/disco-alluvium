using System;
using System.Collections;
using System.Collections.Generic;
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
    private Dictionary<int, Transform> attractingObjects;
    private Dictionary<int, Transform> repellingObjects;

    private bool followingPlayer;
    
    public float movementSpeed = 0.01f;
    public Types myTag;

    public EntityBehaviour myBehaviour;
    public ColorVariants myColor;

    [HideInInspector]
    public bool swapped = false;

    [HideInInspector]
    public int myColorIndex;

    private Rigidbody2D entityRigidbody2D;

    private bool inMouseArea = false;
    private Vector3 targetScale;
    public Vector3 bigScale = new Vector3(1.2f,1.2f,1);

    private void Start()
    {
        targetScale = bigScale;
        attractingObjects = new Dictionary<int, Transform>();
        repellingObjects = new Dictionary<int, Transform>();
        entityRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
        myColorIndex = Random.Range(1, 5);
        switch (myTag)
        {
            case Types.Attract:
            {
                GameManager.instance.amountOfAttracter++;
                break;
            }
            case Types.Nothing:
            {
                GameManager.instance.amountOfNothing++;
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
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel") && myTag != Types.Repel)
        {
            repellingObjects.Add(other.gameObject.GetInstanceID(),thing.transform);
        }

        if (thing.CompareTag("Attract") && myTag != Types.Attract)
        {
            attractingObjects.Add(thing.GetInstanceID(),thing.transform);
        }

        if (thing.CompareTag("Orbit") && myTag != Types.Orbit && !swapped)
        {
            swapped = true;
            if (myBehaviour)
            {
                myBehaviour.player = thing;
                myBehaviour.Switch();   
            }
        }

        if (thing.CompareTag("ColorChange") )//&& myTag != Types.ColorChange)
        {
            if (myColor != null)
            {
                RelationshipBehaviour otherBehaviour = thing.GetComponent<RelationshipBehaviour>();
                myColor.SwitchTo(otherBehaviour.myColorIndex);
                ChangeColorAmount();
            }
        }

        if (thing.CompareTag("Player"))
        {
            if (GameManager.instance.clickActive)
            {
                followingPlayer = !followingPlayer;
                if (followingPlayer)
                    transform.parent.gameObject.layer = 7;
                else
                {
                    transform.parent.gameObject.layer = 8; 
                }
                AddMeToTypeCount();
                ChangeColorAmount();
            }
            else
            {
                inMouseArea = true;
            }
        }
    }

    public void Lerping()
    {
        Vector3 currentScale = transform.localScale;
        if (Vector3.Distance(currentScale,targetScale) > 0.01f)
            currentScale = Vector3.Lerp(currentScale, targetScale, Time.deltaTime);
        else
        {
            if (targetScale == bigScale)
                targetScale = Vector3.one;
            else
                targetScale = bigScale;
        }

        transform.localScale = currentScale;
    }

    public void AddMeToTypeCount()
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
                GameManager.instance.amountOfNothingFollowing++;
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

    public void ChangeColorAmount()
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

    public void RemoveMeFromTypeCount()
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
                GameManager.instance.amountOfNothingFollowing--;
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

    private void OnTriggerExit2D(Collider2D other)
    {
        GameObject thing = other.gameObject;
        if (thing.CompareTag("Repel"))
        {
            repellingObjects.Remove(thing.GetInstanceID());
        }
        
        if (thing.CompareTag("Attract"))
        {
            attractingObjects.Remove(thing.GetInstanceID());
        }

        if (thing.CompareTag("Player") && !followingPlayer)
        {
            transform.localScale = Vector3.one;
            inMouseArea = false;
        }

    }

    private void Update()
    {
        //if (followingPlayer && !GameManager.instance.attractingActive) followingPlayer = false;
        Repel();
        Attract();
        if (inMouseArea)
        {
            Lerping();
            if (GameManager.instance.clickActive)
            {
                followingPlayer = !followingPlayer;
                if (followingPlayer)
                    transform.parent.gameObject.layer = 7;
                else
                {
                    transform.parent.gameObject.layer = 8; 
                }
            }
        }

        
    }

    private void Repel()
    {
        if (repellingObjects.Count == 0) return; 
        Vector2 mainDir = Vector2.zero;
        foreach (var obj in repellingObjects)
        {
            Vector2 dir = (transform.position - obj.Value.position).normalized;
            float dist = Vector2.Distance(obj.Value.position, transform.position);
            mainDir = mainDir + (dir*(1/dist));
        }
        mainDir = mainDir / repellingObjects.Count;
        entityRigidbody2D.velocity = entityRigidbody2D.velocity + (mainDir * movementSpeed);
    }

    private void Attract()
    {
        Vector2 mainDir = Vector2.zero;
        if (attractingObjects.Count != 0)
        {
            
            foreach (var obj in attractingObjects)
            {
                Vector2 dir = (obj.Value.position - transform.position).normalized;
                float dist = Vector2.Distance(obj.Value.position, transform.position);
                mainDir = mainDir + (dir*dist);
            }

            mainDir = mainDir / attractingObjects.Count;
        }
        if (followingPlayer)
        {
            Vector2 dir = (GameManager.instance.player.transform.position - transform.position).normalized;
            float dist = Vector2.Distance(GameManager.instance.player.transform.position, transform.position);
            mainDir = mainDir + (dir * (GameManager.instance.playerAttractionRate * dist));
        }
        entityRigidbody2D.velocity = entityRigidbody2D.velocity + (mainDir * movementSpeed);
    }
}
