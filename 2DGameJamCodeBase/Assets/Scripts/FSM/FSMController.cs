using UnityEngine;

public class FSMController : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;

    public void DoStateTransition(EStates eStates)
    {
        stateMachine.DoStateTransition(eStates);
    }
}
