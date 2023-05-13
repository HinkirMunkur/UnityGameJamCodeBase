using UnityEngine;

public abstract class Priority : MonoBehaviour, IExecuteable
{
    public ETaskStatus CurrentETaskStatus { get; set; } = ETaskStatus.START;
    public abstract ETaskStatus Run();
}
