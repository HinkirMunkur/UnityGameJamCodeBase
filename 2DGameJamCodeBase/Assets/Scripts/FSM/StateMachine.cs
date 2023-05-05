using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public interface IContext
{
    void SetState(EStates newEStates);
    void DoStateTransition(EStates newEStates);
}

[Serializable]
public class StateToClassDictionary
{
    [FormerlySerializedAs("eState")] public EStates eStates;
    public State stateClass;
}

public abstract class StateMachine : MonoBehaviour, IContext
{
    [SerializeField] private List<StateToClassDictionary> mockStateDictionary;
    [SerializeField] private bool isDebugEnabled;
    
    private Dictionary<EStates, State> statesDictionary = new Dictionary<EStates, State>();

    private Dictionary<EStates, Action> stateTransitionDictionary;
    
    private EStates _startEStates;
    
    private State currentState;

    private void Awake()
    {
        foreach (StateToClassDictionary item in mockStateDictionary)
        {
            statesDictionary.Add(item.eStates, item.stateClass);
        }

        stateTransitionDictionary = new Dictionary<EStates, Action>()
        {
            { EStates.NONE, GoNone },
        };
    }

    private void Start()
    {
        SetState(_startEStates);
    }

    private void Do() => currentState.Do();
    private void GoNone() => currentState.GoNone(this);

    public void DoStateTransition(EStates newEStates)
    {
        stateTransitionDictionary[newEStates]?.Invoke();
    }
    
    public void SetState(EStates newEStates)
    {
        currentState = statesDictionary[newEStates];
        Do();
        
        if (isDebugEnabled) { Debug.Log(currentState); }
    }
}
