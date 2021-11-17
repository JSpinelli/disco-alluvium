using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ChargingAction : MonoBehaviour
{
    public abstract void NextStep(float percentage);
    public abstract void Reset();
    public abstract bool isDone();
}
