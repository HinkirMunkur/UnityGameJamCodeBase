using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Serialization;

public class TransitionRefereneces : Singletonn<TransitionRefereneces>
{
    public List<ESceneTransitionToTransition> ESceneTransitionToTransitionList;
}

[Serializable]
public class ESceneTransitionToTransition
{
    public ESceneTransition ESceneTransition;
    public Transition Transition;
    public GameObject BlackScreen;
}
