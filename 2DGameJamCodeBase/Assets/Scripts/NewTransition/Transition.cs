using UnityEngine;
using UnityEngine.UI;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] protected Image blackBackground;

    public Image BlackBackground
    {
        get { return blackBackground; }
        set { blackBackground = value; }
    }

    public abstract void ExecuteCustomStartTransition(float duration);
    public abstract void ExecuteCustomEndTransition(float duration);
    public float TransitionDuration { get; set; }

#if UNITY_EDITOR

    public virtual void SetTransitionReferences(Image blackBG)
    {
        blackBackground = blackBG;
        blackBackground.enabled = false;
    }
    
#endif
}
