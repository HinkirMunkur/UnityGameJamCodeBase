using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareTransition : Transition
{
    [SerializeField] private Animator animator;
    public override void ExecuteCustomStartTransition()
    {
        animator.Play("StartTransition");

    }
    
    public override void ExecuteCustomEndTransition()
    {
        animator.Play("EndTransition");
    }
}
