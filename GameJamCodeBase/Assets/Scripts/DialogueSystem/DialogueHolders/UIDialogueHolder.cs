using UnityEngine;
using UnityEngine.UI;

public class UIDialogueHolder : DialogueHolder
{
    [Space(10)]
    [Header("UIDialogueHolder Parameters")]
    [SerializeField] private Image dialogueHolderImage;
    [SerializeField] private Animator dialogueAnimator;

    private RealUIDialogue realUIDialogue = null;
    private UIDialogue uiDialogue = null;

    protected override void InitReferences(ref RealDialogue realDialogue)
    {
        realDialogue = realUIDialogue;
    }

    protected override void OnStartDialogueActions(Dialogue dialogue)
    {
        realUIDialogue = new RealUIDialogue();
        
        realUIDialogue.Init(dialogue);
        
        uiDialogue = dialogue as UIDialogue;
        
        base.OnStartDialogueActions(dialogue);
    }

    protected override void SetDefaultValues(int index)
    {
        base.SetDefaultValues(index);
        
        realUIDialogue.SetCustomAnimatorStateName(index, 
            uiDialogue.defAnimatorStateNames[uiDialogue.characterCounts[index]]);
    }

    protected override void ControlCustomValues(int index)
    {
        base.ControlCustomValues(index);
        
        if (index < uiDialogue.animatorStateNames.Count)
        {
            realUIDialogue.SetCustomAnimatorStateName(uiDialogue.animatorStateNames[index].id, 
                uiDialogue.animatorStateNames[index].animationStateName);
        }
    }

    protected override RealDialogue OnCustomDialogueActions(int index)
    {
        SetDefaultValues(index);
        ControlCustomValues(index);
        
        HolderOnCustomDialogueActions?.Invoke(realUIDialogue, index); 
        
        base.OnCustomDialogueActions(index);
        
        dialogueHolderImage.sprite = uiDialogue.sprites[uiDialogue.characterCounts[index]];
        dialogueAnimator.runtimeAnimatorController = uiDialogue.animators[uiDialogue.characterCounts[index]];

        dialogueAnimator.Play(realUIDialogue.animatorStateNames[index]);

        return realUIDialogue;
    }

    protected override void OnOneDialogueEndActions()
    {
        dialogueAnimator.Play("NotTalking");
        HolderOnOneDialogueEndActions?.Invoke();
    }

    protected override void OnEndDialogueActions()
    {
        base.OnEndDialogueActions();
    }

}
