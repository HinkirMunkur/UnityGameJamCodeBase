using UnityEngine;
using DG.Tweening;

public class RoundTransition : Transition
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
