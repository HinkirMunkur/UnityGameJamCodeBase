using EasyButtons;
using UnityEngine;

public class PlayerStateMachineTestObject : MonoBehaviour
{
    [SerializeField] private PlayerFSMController playerFsmController;
    [SerializeField] private EPlayerState ePlayerState;
    
    [Button]
    private void StateTransitionTo()
    {
        playerFsmController.DoStateTransition(ePlayerState);
    }
}
