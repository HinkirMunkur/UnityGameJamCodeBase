using UnityEngine;

public abstract class Priority : MonoBehaviour, IExecuteable
{
    public ETaskStatus CurrentETaskStatus { get; set; } = ETaskStatus.START;
    public bool CurrentTaskSuccess { get; set; } = false;
    public abstract ETaskStatus Run();
}
