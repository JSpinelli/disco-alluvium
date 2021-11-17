using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SwitchAction : MonoBehaviour
{
    public abstract bool isDone();
    public abstract void StartAction();
}
