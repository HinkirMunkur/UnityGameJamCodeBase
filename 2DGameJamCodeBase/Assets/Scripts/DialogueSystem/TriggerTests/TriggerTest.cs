using UnityEngine;

public class TriggerTest : MonoBehaviour
{

    [SerializeField] private DialogueHolder dialogueHolder;
    
    [SerializeField] private BoxCollider2D boxCollider;
    
    private bool canStartDialogue = false;
    private bool dialogueStarted = false;

    private bool firstTrigger = true;
    
    
    private void Start()
    {
        dialogueHolder.HolderOnStartDialogueActions += HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions += HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions += HolderOnEndDialogueActions;
    }
    
    private void Update()
    {
        if (dialogueStarted)
        {
            if(Input.GetKeyDown(KeyCode.Space))
                DialogueTrigger.Instance.TriggerDialogue();
        }

        if (canStartDialogue)
        {
            dialogueStarted = true;
            canStartDialogue = false;
            DialogueTrigger.Instance.TriggerDialogue();
        }


    }

    private void OnDestroy()
    {
        dialogueHolder.HolderOnStartDialogueActions -= HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions -= HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions -= HolderOnEndDialogueActions;
    }

    private void HolderOnStartDialogueActions()
    {

    }

    private void HolderOnCustomDialogueActions(RealDialogue realDialogue, int index)
    {

    }

    private void HolderOnEndDialogueActions()
    {
        Destroy(this.gameObject);
        
        dialogueHolder.HolderOnStartDialogueActions -= HolderOnStartDialogueActions;
        dialogueHolder.HolderOnCustomDialogueActions -= HolderOnCustomDialogueActions;
        dialogueHolder.HolderOnEndDialogueActions -= HolderOnEndDialogueActions;
    }
    
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (firstTrigger)
        {
            firstTrigger = false;
            
            dialogueHolder.HolderOnStartDialogueActions += HolderOnStartDialogueActions;
            dialogueHolder.HolderOnCustomDialogueActions += HolderOnCustomDialogueActions;
            dialogueHolder.HolderOnEndDialogueActions += HolderOnEndDialogueActions;
        }

        canStartDialogue = true;
    }
    
    private void OnTriggerExit2D(Collider2D col)
    {
        canStartDialogue = false;
    }

}
