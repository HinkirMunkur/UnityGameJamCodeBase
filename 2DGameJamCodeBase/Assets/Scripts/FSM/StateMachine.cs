using System.Collections.Generic;
using UnityEngine;
using System;

public interface IContext<EState>
{
    void SetState(EState newEStates);
    void DoStateTransition(EState newEStates);
}

namespace Munkur
{
    public abstract class StateMachine<EState> : MonoBehaviour, IContext<EState>
    {
        [SerializeField] private List<StateToClassDictionary> mockStateDictionary;
        [SerializeField] private bool isDebugEnabled;
    
        protected Dictionary<EState, State> statesDictionary;

        protected Dictionary<EState, Action> stateTransitionDictionary;

        protected State currentState;
    
        [Serializable]
        public class StateToClassDictionary
        {
            public EState eStates;
            public State stateClass;
        }
    
        protected virtual void Awake()
        {
            statesDictionary = new Dictionary<EState, State>();
        
            foreach (StateToClassDictionary item in mockStateDictionary)
            {
                statesDictionary.Add(item.eStates, item.stateClass);
            }
        }

        private void Do() => currentState.Do();

        public void DoStateTransition(EState newEStates)
        {
            stateTransitionDictionary[newEStates]?.Invoke();
        }
    
        public void SetState(EState newEStates)
        {
            currentState = statesDictionary[newEStates];
            Do();
        
            if (isDebugEnabled) { Debug.Log(currentState); }
        }
    }
}