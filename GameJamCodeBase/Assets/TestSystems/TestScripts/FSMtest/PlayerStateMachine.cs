using System.Collections.Generic;
using UnityEngine;
using System;
using Munkur;

public enum EPlayerState
{
    IDLE,
    RUN
}

public class PlayerStateMachine : StateMachine<EPlayerState>
{
    [SerializeField] private EPlayerState startEStates;

    protected override void Awake()
    {
        base.Awake();

        stateTransitionDictionary = new Dictionary<EPlayerState, Action>()
        {
            { EPlayerState.IDLE, GoIdle },
            { EPlayerState.RUN, GoRun }
        };
    }

    private void Start()
    {
        SetState(startEStates);
    }
    
    private void GoIdle() => ((PlayerState)currentState).GoIdle(this);
    private void GoRun() => ((PlayerState)currentState).GoRun(this);
}
