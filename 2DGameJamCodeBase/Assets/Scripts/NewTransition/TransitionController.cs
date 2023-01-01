using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private List<ESceneTransitionToTransition> _ESceneTransitionToTransitionList;

    public static Dictionary<ESceneTransition, Transition> TransitionHolderDictionary =
        new Dictionary<ESceneTransition, Transition>();

    private void Awake()
    {
        foreach (var _ESceneTransitionToTransition in _ESceneTransitionToTransitionList)
        {
            TransitionHolderDictionary.Add(
                _ESceneTransitionToTransition.ESceneTransition,
                _ESceneTransitionToTransition.Transition);
        }
    }
}

[Serializable]
public class ESceneTransitionToTransition
{
    public ESceneTransition ESceneTransition;
    public Transition Transition;
}