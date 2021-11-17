using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorFlick : SwitchAction
{

    public SpriteRenderer sr;
    private Color startColor;
    public Color targetColor;
    public float timeToTransition;
    private float timer;
    private bool timerStarted;


    private void Start()
    {
        timer = 0;
        startColor = sr.color;
    }

    public override bool isDone()
    {
        return timer >= timeToTransition;
    }

    public override void StartAction()
    {
        timerStarted = true;
    }

    private void Update()
    {
        if (timerStarted && timer < timeToTransition)
        {
            sr.color = Color.Lerp(startColor, targetColor, timer / timeToTransition);
            timer += Time.deltaTime;
            if (timer >= timeToTransition)
                timerStarted = false;
        }
    }
}
