using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    public abstract void ExecuteCustomStartTransition();
    public abstract void ExecuteCustomEndTransition();
    public float TransitionDuration { get; set; }
}
