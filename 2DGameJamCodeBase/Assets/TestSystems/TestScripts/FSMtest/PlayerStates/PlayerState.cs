
public abstract class PlayerState : State
{
    public virtual void GoIdle(IContext<EPlayerState> context) { context.SetState(EPlayerState.IDLE); }
    public virtual void GoRun(IContext<EPlayerState> context) { context.SetState(EPlayerState.RUN); }
}
