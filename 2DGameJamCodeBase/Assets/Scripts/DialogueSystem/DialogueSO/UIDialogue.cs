using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIDialogueData", menuName = "ScriptableObjects/UIDialogueDataScriptableObject", order = 1)]
public class UIDialogue : Dialogue
{
    [Header("UIDialogue Essential Values")] 
    [Space(10)]
    public Sprite[] sprites;
    public RuntimeAnimatorController[] animators;

    [Header("UIDialogue Default Values")] 
    [Space(10)]
    public List<string> defAnimatorStateNames;

    [Header("UIDialogue Specific Values")]
    [Space(10)]
    public List<AnimatorStateNamesDic> animatorStateNames;
}

public class RealUIDialogue : RealDialogue
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