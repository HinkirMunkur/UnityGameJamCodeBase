using UnityEngine;
using TMPro;

public class TriggerKey : MonoBehaviour
{
    [SerializeField] private DialogueHolder dialogueHolder;

    [SerializeField] private TMP_Text dialogueText;

    private void Start()
    {
        dialogueHolder.HolderOnStartDialogueActions += HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions += HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions += HolderOnEndDialogueActions;
    }

    private void OnDestroy()
    {
        dialogueHolder.HolderOnStartDialogueActions -= HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions -= HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions -= HolderOnEndDialogueActions;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            DialogueTrigger.Instance.TriggerDialogue();

    }
    
    private void HolderOnStartDialogueActions()
    {
        
    }
    
    private void HolderOnCustomDialogueActions(RealDialogue realDialogue, int index)
    {

    }
    
    private void HolderOnEndDialogueActions()
    {
    }
    

}
