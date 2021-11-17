using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleChange : ChargingAction
{
    private Vector3 startingScale;
    public Vector3 targetScale;
    public GameObject entity;

    private void Start()
    {
        startingScale = entity.transform.localScale;
    }

    public override void NextStep(float percentage)
    {
        entity.transform.localScale = Vector3.Lerp(startingScale, targetScale,percentage);
    }

    public override void Reset()
    {
        entity.transform.localScale = startingScale;
    }

    public override bool isDone()
    {
        return entity.transform.localScale == targetScale;
    }
}
