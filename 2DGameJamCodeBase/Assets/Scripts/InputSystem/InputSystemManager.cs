using UnityEngine;

public class InputSystemManager : SingletonnPersistent<InputSystemManager>
{
    [SerializeField] protected bool enableInputListener = false;
    
    public bool EnableInputListener => enableInputListener;
}
