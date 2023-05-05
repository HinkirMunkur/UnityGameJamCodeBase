using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UISimpDialogueData", menuName = "ScriptableObjects/UISimpDialogueDataScriptableObject", order = 1)]
public class UISimpDialogue : Dialogue
{
    [Header("UISimpDialogue Essential Values")] 
    [Space]
    public RuntimeAnimatorController[] animators;
    
    [Header("UISimpDialogue Default Values")] 
    [Space]
    public List<string> defAnimatorStateNames;

    [Header("UISimpDialogue Specific Values")]
    [Space]
    public List<AnimatorStateNamesDic> animatorStateNames;
}

public class RealUISimpDialogue : RealDialogue
{
    public string[] animatorStateNames;

    public override void Init(Dialogue dialogue)
    {
        base.Init(dialogue);
        
        int arraysLength = dialogue.sentences.Length;

        animatorStateNames = new string[arraysLength];
    }

    public void SetCustomAnimatorStateName(int index, string customAnimatorStateName)
    {
        animatorStateNames[index] = customAnimatorStateName;
    }

}
