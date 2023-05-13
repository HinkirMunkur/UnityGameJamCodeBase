using UnityEngine;

public class UISimpDialogueHolder : DialogueHolder
{
    [Space(10)]
    [Header("UISimpDialogueHolder Parameters")]
    [SerializeField] private Animator dialogueAnimator;

    private RealUISimpDialogue realUISimpDialogue = null;
    private UISimpDialogue uiSimpDialogue = null;
    
    protected override void InitReferences(ref RealDialogue realDialogue)
    {
        realDialogue = realUISimpDialogue;
    }

    protected override void OnStartDialogueActions(Dialogue dialogue)
    {
        realUISimpDialogue = new RealUISimpDialogue();
        
        realUISimpDialogue.Init(dialogue);
        
        uiSimpDialogue = dialogue as UISimpDialogue;
        
        base.OnStartDialogueActions(dialogue);
    }
    
    protected override void SetDefaultValues(int index)
    {
        base.SetDefaultValues(index);
        
        realUISimpDialogue.SetCustomAnimatorStateName(index, 
            uiSimpDialogue.defAnimatorStateNames[uiSimpDialogue.characterCounts[index]]);
    }

    protected override void ControlCustomValues(int index)
    {
        base.ControlCustomValues(index);
        
        if (index < uiSimpDialogue.animatorStateNames.Count)  
            realUISimpDialogue.SetCustomAnimatorStateName(uiSimpDialogue.animatorStateNames[index].id, uiSimpDialogue.animatorStateNames[index].animationStateName);
    }

    protected override RealDialogue OnCustomDialogueActions(int index)
    {
        SetDefaultValues(index);
        ControlCustomValues(index);
        
        HolderOnCustomDialogueActions?.Invoke(realUISimpDialogue, index);

        base.OnCustomDialogueActions(index);

        dialogueAnimator.runtimeAnimatorController = uiSimpDialogue.animators[uiSimpDialogue.characterCounts[index]];
        dialogueAnimator.Play(realUISimpDialogue.animatorStateNames[index]);

        return realUISimpDialogue;
    }

    protected override void OnOneDialogueEndActions()
    {
        HolderOnOneDialogueEndActions?.Invoke();
    }

    protected override void OnEndDialogueActions()
    {
        base.OnEndDialogueActions();
    }

}
