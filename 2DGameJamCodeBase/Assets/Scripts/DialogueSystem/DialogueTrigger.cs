using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : Singletonn<DialogueTrigger>
{
    [Space(5)]
    [Header("Add Dialogue Scriptable Objects In Executing Order")]
    [Space(3)]
    [SerializeField] List<Dialogue> dialogueList;
    
    [Space(5)]
    
    [Header("Target DialogueHolder Index on  --DialogueManager--")]
    [Space(3)]
    [SerializeField] List<int> targetDialogueHolderIndex;

    private int index = 0;
    
    /// <summary>
    ///   <para> Sets the given dialogue order in --dialogueList-- </para>
    /// </summary>
    /// <param name="newDialogue"> Dialogue Scriptable Object to be added </param>
    /// <param name="effectDialogueHolderIndex"> Dialogue Holder (in Dialogue Manager) to be affected </param>
    /// <param name="addDialogueIndex"> Order in --dialogueList-- </param>
    public void SetDialogue(Dialogue newDialogue, int effectDialogueHolderIndex, int addDialogueIndex = -1)
    {
        if (addDialogueIndex == -1)
        {
            dialogueList.Insert(dialogueList.Count, newDialogue);
        }
        else
        {
            dialogueList.Insert(addDialogueIndex, newDialogue);
        }
        
        targetDialogueHolderIndex.Add(effectDialogueHolderIndex);
    }
    
    /// <summary>
    ///   <para> Triggers the Dialogue order of --dialogueList-- </para>
    /// </summary>
    public void TriggerDialogue()
    {
        if(DialogueManager.Instance.IsDialogueStarted || dialogueList.Count > 0)
        {
            if (!DialogueManager.Instance.IsDialogueStarted)
            {
                Dialogue dialogue = dialogueList[0];
                dialogueList.RemoveAt(0);
                
                DialogueManager.Instance.StartDialogue(dialogue, targetDialogueHolderIndex[index++]);
            }
            else
            {
                DialogueManager.Instance.DisplayNextSentence();
            }
        }
    }   

}
