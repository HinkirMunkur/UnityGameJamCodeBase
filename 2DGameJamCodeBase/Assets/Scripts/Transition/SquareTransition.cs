using UnityEngine;
using DG.Tweening;

public class SquareTransition : Transition
{
    public override void ExecuteCustomStartTransition(float duration)
    {
        blackBackground.enabled = true;
        blackBackground.transform.DOScale(Vector3.zero, duration);
    }
    
    public override void ExecuteCustomEndTransition(float duration)
    {
        blackBackground.transform.localScale = Vector3.zero;
        blackBackground.enabled = true;
        
        blackBackground.transform.DOScale(Vector3.one, duration);
    }
}
