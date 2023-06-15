using UnityEngine;

public abstract class State : MonoBehaviour
{
    public abstract void Do();
    public virtual void Done() {  }
}
