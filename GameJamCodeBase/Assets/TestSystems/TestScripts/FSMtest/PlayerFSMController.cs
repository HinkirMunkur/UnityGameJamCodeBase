using UnityEngine;

public class PlayerFSMController : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine playerStateMachine;

    public void DoStateTransition(EPlayerState ePlayerState)
    {
        playerStateMachine.DoStateTransition(ePlayerState);
    }
}
