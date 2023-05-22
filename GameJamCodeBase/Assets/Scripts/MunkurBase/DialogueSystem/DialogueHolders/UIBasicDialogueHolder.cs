using UnityEngine;

public class UIBasicDialogueHolder : DialogueHolder
{
    private RealUIBasicDialogue realUIBasicDialogue = null;
    private UIBasicDialogue uiBasicDialogue = null;
    
    protected override void InitReferences(ref RealDialogue realDialogue)
    {
        realDialogue = realUIBasicDialogue;
    }
    
    protected override void OnStartDialogueActions(Dialogue dialogue)
    {
        realUIBasicDialogue = new RealUIBasicDialogue();
        
        realUIBasicDialogue.Init(dialogue);
        
        uiBasicDialogue = dialogue as UIBasicDialogue;
        
        base.OnStartDialogueActions(dialogue);
    }

    protected override RealDialogue OnCustomDialogueActions(int index)
    {
        SetDefaultValues(index);
        ControlCustomValues(index);
        
        HolderOnCustomDialogueActions?.Invoke(realUIBasicDialogue, index);

        base.OnCustomDialogueActions(index);

        return realUIBasicDialogue;
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
