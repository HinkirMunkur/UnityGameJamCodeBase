using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerKey : MonoBehaviour
{
    [SerializeField] private DialogueHolder dialogueHolder;

    [SerializeField] private TMP_Text dialogueText;

    [SerializeField] private Sprite[] sprites;

    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private Animator backGroundAnimator;
    
    [SerializeField] private Transition transition;

    [SerializeField] private GameObject spaceFortexts;

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
        spaceFortexts.SetActive(false);
    }
    
    private void HolderOnCustomDialogueActions(RealDialogue realDialogue, int index)
    {
        if (index == 5)
        {
            backGroundAnimator.enabled = true;
            StartCoroutine(WaitAndFinish());
        }
        else if (index <= 5)
        {
            spriteRenderer.sprite = sprites[index];
        }
        
        spriteRenderer.sprite = sprites[index];
    }

    IEnumerator WaitAndFinish()
    {
        yield return new WaitForSeconds(5f);
        
        // CHANGE
        //StartCoroutine(transition.EndTransition("Credits"));

    }

    private void HolderOnEndDialogueActions()
    {
        
    }
    

}
