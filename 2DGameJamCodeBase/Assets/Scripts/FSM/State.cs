using UnityEngine;

public enum EStates
{
    NONE,
}

public abstract class State : MonoBehaviour
{
    public abstract void Do();
    public virtual void GoNone(IContext context) { context.SetState(EStates.NONE); }
}
