using UnityEngine;

public interface IExecuteable
{
    public ETaskStatus CurrentETaskStatus
    {
        get;
        set;
    }
    public ETaskStatus Run();
}

public enum ETaskStatus
{
    START,
    CONTINUE,
    FINISH,
}

public abstract class Task : MonoBehaviour, IExecuteable
{
    public ETaskStatus CurrentETaskStatus { get; set; } = ETaskStatus.START;
    public abstract ETaskStatus Run();
}
