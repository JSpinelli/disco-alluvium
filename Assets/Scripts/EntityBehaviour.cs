using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    public List<BezierTraversal> idleMovement;
    public List<BezierTraversal> followingMovement;

    public float waitingAmount;
    private float timer;
    private bool onTarget;
    private bool waitingForSwitch;

    public ChargingAction chargingAction;
    public List<SwitchAction> switchActions;

    private int currentTransition;
    private int totalTransitions;

    private bool followingTarget = false;

    [HideInInspector]
    public GameObject player;

    private void Start()
    {
        foreach (var mvt in idleMovement)
        {
            mvt.StopResume();
        }

        currentTransition = 0;
        totalTransitions = switchActions.Count;
        waitingForSwitch = true;
        player = GameManager.instance.player;
    }

    private void Update()
    {
        if (onTarget && waitingForSwitch && !followingTarget)
        {
            timer += Time.deltaTime;
            if (chargingAction)
                chargingAction.NextStep(timer/waitingAmount);
            
            if (timer > waitingAmount)
            {
                if(switchActions.Count >0)
                    switchActions[currentTransition].StartAction();
                if (chargingAction)
                    chargingAction.Reset();
                waitingForSwitch = false;
            }
        }
        if (!waitingForSwitch && currentTransition <= totalTransitions && !followingTarget)
        {
            if (currentTransition == totalTransitions)
            {
                Switch();
                currentTransition++;
            }
            else
            {
                if (switchActions[currentTransition].isDone())
                {
                    currentTransition++;
                    if (currentTransition != totalTransitions)
                        switchActions[currentTransition].StartAction();
                }
            }
        }

        if (followingTarget)
        {
            transform.parent.position = player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            onTarget = true;
    }

    public void Switch()
    {
        foreach (var mvt in idleMovement)
        {
            mvt.StopResume();
        }
        
        foreach (var mvt in followingMovement)
        {
            mvt.StopResume();
        }

        followingTarget = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (chargingAction)
                chargingAction.Reset();
            onTarget = false;
            timer = 0;   
        }
    }
}
