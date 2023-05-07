using UnityEngine;

[CreateAssetMenu(fileName = "UIDBasicialogueData", menuName = "DialogueSO/UIBasicDialogueDataScriptableObject", order = 1)]
public class UIBasicDialogue : Dialogue
{

}

public class RealUIBasicDialogue : RealDialogue
{

    public override void Init(Dialogue dialogue)
    {
        base.Init(dialogue);
    }

}
